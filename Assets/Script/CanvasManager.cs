using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using System;

public class CanvasManager : MonoBehaviour,IPointerDownHandler,IBeginDragHandler, IEndDragHandler, IPointerUpHandler
{
    #region Debug
    public RectTransform DebugCanvas;
    public Toggle FrameRateToggle;
    public Text FrameRateText;
    IEnumerator FrameRateCoroutine;

    public Text SetReslitionWidth;
    public Text SetReslitionHeight;
    //Time DEBUG
    int Tick;
    int HighestFPS;
    int LowestFPS;
    int AverageFPS;
    int LastFPS;
    int total;

    #endregion
    [SerializeField]
    int defultHideBartime, defultShowBartime;

    int ShowBarTime=10,HideBarTime = 10;
    [SerializeField]
    UIClinet[] SectionUIClinet;
    IEnumerator coroutine,HideCoroutine;
    public VideoPlayer ScreenProtectVideo;
    public bool bScreenProtect;
    [SerializeField]
    public int DefaultScreenWidth, DefaultScreenHeight,ScreenProtectWaitTime;
    public static CanvasManager instance;


    // [SerializeField]
    // private GraphicRaycaster DebugCanvasGraphicRayCaster/*, ScreenProtectGraphicRaycast*/;
    //[SerializeField]
    //public Color[] color = new Color[10];

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
       }
        coroutine = ShowBar();
        HideCoroutine = HideBar();
        StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DebugCanvas.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (MainTitleAinamtionCtr.instance.isShowMaintitle)
            {
				foreach (var item in ReadJson.UIclinets) {
					if (!item.enableIneraction) {
					
						return;
					} else {
						HideBarTime = 60;//设置回到屏保界面时间为60秒；
					}
				}
              
            }
            else
            {
                //ShowBarTime = 1;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(3, true);
        //    MainTitleAinamtionCtr.instance.isShowMaintitle = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.W))
        //{
        //    MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(3, false);
        //    MainTitleAinamtionCtr.instance.isShowMaintitle = false;
        //}

    }

    public void OnEndDrag(PointerEventData eventData)
    {
      //  ScreenProtect();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    //    ScreenProtect();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
     //   ScreenProtect();
    }


    public void OnPointerDown(PointerEventData eventData)
    {


        //  ScreenProtect();
    }

    public int RandomInt(float min, float Max) {
        float fvalue = UnityEngine.Random.Range(min, Max);
       // Debug.Log(fvalue);
    int  value = Mathf.FloorToInt(fvalue);
     //   Debug.Log(value);
        return value;
    }

    public IEnumerator ShowBar()
    {
		while (ShowBarTime > 0) {
			yield return new WaitForSeconds (1);
			//   print(ShowBarTime);
			ShowBarTime--;

			Debug.Log ("ShowBarTime_" + ShowBarTime.ToString ());


            if (ShowBarTime == 0) {
                // Debug.Log("Show Bar");
                int value;
                value = RandomInt(1, 3);
                MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(value, true);
                MainTitleAinamtionCtr.instance.isShowMaintitle = true;
                HideBarTime = defultHideBartime;
                StartCoroutine(HideBar());

            }
        }

    }

    public IEnumerator HideBar() {
        while (HideBarTime > 0)
        {
            yield return new WaitForSeconds(1);
         //   print(HideBarTime);
            HideBarTime--;
			Debug.Log ("HideBarTime_"+HideBarTime.ToString());
			if (HideBarTime == 18) {		
				foreach (var item in ReadJson.UIclinets) {
					Debug.Log ("enableAllBtn");
					item.enableIneraction = true;
				}
			}
				
			if (HideBarTime == 1){

				foreach (var item in ReadJson.UIclinets) {
					Debug.Log ("disableAllBtn");
					item.enableIneraction = false;
				}
			}

            if (HideBarTime == 0)
            {
                //  Debug.Log("Hide Bar");
                int value;
                value = RandomInt(1, 3);
                MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(value, false);
                MainTitleAinamtionCtr.instance.isShowMaintitle = false;
                ShowBarTime = defultShowBartime;
                StartCoroutine(ShowBar());

            }
        }
    }

        public void showFrameRate() {
        if (FrameRateToggle.isOn)
        {
            StartCoroutine(UpdateUpdateFrameRate(1f));
            FrameRateText.gameObject.SetActive(FrameRateToggle.isOn);
        }
        else {
            StopCoroutine(UpdateUpdateFrameRate(1f));
            FrameRateText.gameObject.SetActive(FrameRateToggle.isOn);
        }
    }


    public void setResolution() {
        int width = int.Parse( SetReslitionWidth.text);
        int height = int.Parse(SetReslitionHeight.text);
        Screen.SetResolution(width, height, false,120);
        //Debug.Log("set res");
    }

    public IEnumerator UpdateUpdateFrameRate(float waitTime)
    {
        AverageFPS = HighestFPS = LowestFPS = (int)(1f / Time.deltaTime);
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Tick += 1 * (int)waitTime;
            int currentFPS = (int)(1f / Time.deltaTime);

            total += LastFPS;
            if (currentFPS > HighestFPS)
            {
                HighestFPS = currentFPS;
            }

            if (currentFPS < LowestFPS)
            {
                LowestFPS = currentFPS;
            }

            int DisplayAverage = total / Tick;



            string FCurrentFPS = (currentFPS).ToString() + "FPS";
            string FLowestFPS = LowestFPS.ToString() + "FPS";
            string FHighestFPS = HighestFPS.ToString() + "FPS";
            string FaverageFPS = DisplayAverage.ToString() + "FPS";
            string Ftime = Tick.ToString() + "SEC";
            FrameRateText.text = "Current: " + FCurrentFPS + "\n" + "Lowest: " + FLowestFPS + "\n" + "Heighest: " + FHighestFPS + "\n" + "AverageFPS: " + FaverageFPS + "\n " + "Time: " + Ftime;
            LastFPS = currentFPS;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ResetApplication()
    {
        for (int i = 0; i < SectionUIClinet.Length; i++)
        {
            SectionUIClinet[i].ResetProgram();
        }
    }

    public void CloseDebugConsole() {
        DebugCanvas.gameObject.SetActive(false);
    }




    void RawImageAlphaLerp(RawImage rawImage,float TargetAlphaValue, float delaytime,float time) {
        LeanTween.value(rawImage.color.a, TargetAlphaValue, time).setDelay(delaytime).setEaseInQuad().setOnUpdate((float value) =>
        {
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, value);
        });
    }

    void ImageAlphaLerp(RawImage Image, float TargetAlphaValue, float delaytime, float time)
    {
        LeanTween.value(Image.color.a, TargetAlphaValue, time).setDelay(delaytime).setEaseInQuad().setOnUpdate((float value) =>
        {
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, value);
        });
    }


}
