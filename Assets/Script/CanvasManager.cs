using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
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
    int ScreenProtecttick=0;
    [SerializeField]
    UIClinet[] SectionUIClinet;
    IEnumerator coroutine;
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
    void Start () {
        if(instance == null)
        {
            instance = this;

        }
        coroutine = ScreenProtectIE(ScreenProtectWaitTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            DebugCanvas.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0)) {
            MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(1, true);
            MainTitleAinamtionCtr.instance.isShowMaintitle = true;
            ScreenProtecttick = 0;
            Debug.Log("hit");
            if (MainTitleAinamtionCtr.instance.isShowMaintitle)
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
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

    //void SetGraphicRayCaster(GraphicRaycaster raycaster,bool active) {
    //    raycaster.enabled = active;
    //}






    public IEnumerator ScreenProtectIE(float _WaitTime,int _tick = 0) {
        ScreenProtecttick = _tick;
        while (MainTitleAinamtionCtr.instance.isShowMaintitle)
        {
            ScreenProtecttick++;
            Debug.Log(ScreenProtecttick);
            yield return new WaitForSeconds(1);
            if (ScreenProtecttick == _WaitTime) {
                MainTitleAinamtionCtr.instance.ShowAndHideMainTitle(1, false);
                MainTitleAinamtionCtr.instance.isShowMaintitle = false;
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
        Debug.Log("set res");
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
