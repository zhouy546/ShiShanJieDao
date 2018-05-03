using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(MouseBehavior)), RequireComponent(typeof(GraphicRaycaster)), RequireComponent(typeof(Canvas))]
public class UIClinet : MonoBehaviour, IPointerDownHandler
{
    #region Para

    //VideoClip videoClip;
    //VideoPlayer myVideoPlayer;
    public GameObject MainBigTitlePlane;
   // private IEnumerator coroutine;
    public delegate void initializationUItext();
    public  event initializationUItext UpdateUIeventtext;
    public delegate void initializationUImage();
    public  event initializationUItext UpdateUIeventImage;
    int[] tempint;

    private int FrontAndBackInt = -1;
    delegate void setupBottomImage(int i, Information item,bool isback, Image[] BackImageOrFrotImage);
    //public bool Display;
    public float UnitDisplayWidth;
    public float DefaultXpos;
    [SerializeField]
    public Canvas canvas;
    [System.Serializable]
    public struct info{
   
        

        public int DefaultID;
        public int CurrentID;

        public int DefaultSubtitleNum;
        public int CurrentSubtitleNum;

        public string DefaultTitle;
        public string CurrentTitle;


        public Text ClickBarBigTitle;
        public Text BigTitle;
        public Image BigTitleImage;
        public Text BigTitleDiscription;

        public Text SubTitle;
        public Text MainContent;

        public Image[] BottomFrontImage;
        public Image[] BottomBackImage;

        public List<string> BottomTitleSelection;
        public string[] BackBottomTitleSelection;
        public string[] tempF;
        public Text[] BottomTitleText;
        public float ImageFadeTime;


        //public Dictionary<string,Image> BottomDisplay


        public RectTransform[] TopImagePos;
        public RectTransform[] TopimageSlot;
    }
    public RightMenuBar rightMenuBar;
    public LoccaterManager loccaterManager;
    public AVPlayOnUGUI AVplayOnUGUI;
    public GameObject DisplayLayer;
    public GameObject[] MidInfo = new GameObject[2];
    public TextChangeLine textChangeLine;
    public info myinfo;

    #endregion

    // Use this for initialization
    void Start () {
        // coroutine = UpdateBottomSection(5.0f);
        //StartCoroutine(coroutine);

    }

    public void setupDefalutIDandBigTitle(int value) {
        //Debug.Log(value);
        myinfo.DefaultID = value;
        myinfo.DefaultTitle =  ReadJson.instance.myinformationList[value].ID_BigTitledictionary[value];
    //    myVideoPlayer = GetComponent<VideoPlayer>();
        //AssignVideoClip();
        this.GetComponent<MouseBehavior>().enabled = false;
        ResetProgram();
    }

    public void ResetProgram() {
    
        this.name = myinfo.DefaultTitle;
        myinfo.CurrentTitle = myinfo.DefaultTitle;
        myinfo.CurrentID = myinfo.DefaultID;
        myinfo.CurrentSubtitleNum = myinfo.DefaultSubtitleNum;
        StartCoroutine(SubScribeUpdateUIevent());
        if ((myinfo.DefaultID % 2) == 0) {
            DefaultXpos = 0;
        }
        else {
            DefaultXpos = UnitDisplayWidth/2;
        }
        SetxPos(DefaultXpos);

        //set Main bighTitle Color
       
         MainBigTitlePlane.GetComponent<Image>().sprite =ReadJson.instance.myinformationList[myinfo.DefaultID].MainTitleImage;
        canvas.sortingOrder = 1;
    }

    //void AssignVideoClip() {
    //    if (myinfo.DefaultID % 2 == 0) {
    //        myVideoPlayer.clip = GetTheVideoClip(myinfo.DefaultTitle);
    //    }
    //}


    //VideoClip GetTheVideoClip(string s) {
    //    string path = "Video/" + s;
    //    videoClip = Resources.Load<VideoClip>(path);

    //    return videoClip;

    //}

    private void OnEnable()
    {


    }

    IEnumerator SubScribeUpdateUIevent()
    {
        yield return new WaitForSeconds(.5f);
        UpdateUI();

    }
    #region event and delegate
    public void UpdateUI()
    {
        UpdateUIeventtext += SetupText;
        UpdateUItext();
        UpdateUIeventImage += SetupImage;
        UpdateUIimage();

    }

    public void UpdateUItext()
    {
        if (UpdateUIeventtext != null)
        {
            UpdateUIeventtext();
        }

    }

    public void UpdateUIimage()
    {
        if (UpdateUIeventImage != null)
        {
            UpdateUIeventImage();
        }

    }

    private void OnDisable()
    {
        UpdateUIeventtext -= SetupText;
        UpdateUIeventImage -= SetupImage;
    }

    #endregion

    #region setupGUI

    //generate gui

    void SetupText() {

        SetupMidToptext();
      //  SetupBottomText();

     //   Debug.Log("bigtitle" + myinfo.BigTitle.text);
    }

    

    void SetupMidToptext()
    {
        myinfo.ClickBarBigTitle.name=myinfo.ClickBarBigTitle.text = myinfo.BigTitle.text = myinfo.CurrentTitle;//setup default BigTitle
        myinfo.SubTitle.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum];//setup subtitle
        myinfo.MainContent.text = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_MainContentdictionary[myinfo.SubTitle.text];//setup main content
        //setupRightButtonText
    }

    //public IEnumerator UpdateBottomSection(float waitTime) {
    //    while (true)
    //    {
    //        //Debug.Log("start corutine");
    //        yield return new WaitForSeconds(waitTime);
    //        SetupBottomText();
    //        SetupBottomBarImage();
    //    }
    //} 

    //void SetupBottomText() {
    //    myinfo.tempF = GetBottomText(myinfo.BottomTitleSelection);
    //    myinfo.BackBottomTitleSelection = GetBottomText(myinfo.BottomTitleSelection);

    //    for (int i = 0; i < myinfo.BottomTitleText.Length; i++)
    //    {
    //        myinfo.BottomTitleText[i].text = myinfo.tempF[i];
    //    }
    //}

    //string[] GetBottomText(List<string> _BottomTitleSelection) {
    //    string[] temp;
    //    _BottomTitleSelection.Clear();
    //    List<string> templist = new List<string>();
    //    for (int i = 0; i < ReadJson.instance.myinformationList.Count; i++)//add title to list 
    //    {
    //        _BottomTitleSelection.Add(ReadJson.instance.myinformationList[i].BigTitle);
            
    //    }

    //    //take out it's self
    //    _BottomTitleSelection.Remove(myinfo.CurrentTitle);
    //    tempint = getDifferentNumInRange(0, _BottomTitleSelection.Count-1, myinfo.BottomTitleText.Length);//random 3 different num 

    //    for (int i = 0; i < tempint.Length; i++)//set 3different title
    //    {
    //        templist.Add(_BottomTitleSelection[tempint[i]]);
    //    }

    //    temp = templist.ToArray();
    //    _BottomTitleSelection.Clear();
    //    templist.Clear();
    //    return temp;
    //}

    void SetupImage() {
        setupMidTopImage();
       // SetupBottomBarImage();
    }

    void setupMidTopImage() {
        Sprite temp = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_SubImagedictionary[myinfo.SubTitle.text];
        //if (myinfo.CurrentID == 0) {
        //    //  Debug.Log(ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum]);
        //    Debug.Log(myinfo.CurrentSubtitleNum);
        //}
        loccaterManager.HighlightBtn(ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum]);
        myinfo.BigTitleImage.sprite = temp;
        SetupTopBarImage();
    }

    //void SetupBottomBarImage()
    //{
    //    FrontAndBackInt = NumPositiveAndNegitive(FrontAndBackInt);
    //    if (FrontAndBackInt > 0)
    //    {
    //        SetupBottomBarImage(setupImage, true, false, myinfo.BottomFrontImage);
    //        SetupBottomBarImage(setupImage, false, true, myinfo.BottomBackImage);
    //        //show FrontImage            //front fade in 
    //        FadeAndshowImage(myinfo.BottomFrontImage, 0f, 1f, myinfo.ImageFadeTime);
    //        //back fade out
    //        FadeAndshowImage(myinfo.BottomBackImage, 1f, 0f, myinfo.ImageFadeTime);
    //    }
    //    else
    //    {
    //        //front fade out 
    //        SetupBottomBarImage(setupImage, true, true, myinfo.BottomFrontImage);
    //        SetupBottomBarImage(setupImage, false, false, myinfo.BottomBackImage);
    //        //show FrontImage                       //front fade out 
    //        FadeAndshowImage(myinfo.BottomFrontImage, 1f, 0f, myinfo.ImageFadeTime);
    //        //back fade in
    //        FadeAndshowImage(myinfo.BottomBackImage, 0f, 1f, myinfo.ImageFadeTime);
    //    }


    //}

  public  void FadeAndshowImage(Image[] image,float from, float to, float time)
    {
        LeanTween.value(from, to, time).setOnUpdate((float value) =>
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].color = new Color(1f, 1f, 1f, value);
            }
        });

    }


  public  void FadeAndshowImage(Image image, float from, float to, float time)
    {
        LeanTween.value(from, to, time).setOnUpdate((float value) =>
        {

                image.color = new Color(1f, 1f, 1f, value);

        });

    }

   public void FadeAndshowRawImage(RawImage image, float from, float to, float time)
    {
        LeanTween.value(from, to, time).setOnUpdate((float value) =>
        {
                image.color = new Color(1f, 1f, 1f, value);
        });

    }

    int NumPositiveAndNegitive(int x)
    {
        return -x;
    }


    public void SetupTopBarImage()//keepworking
    {
            int index = MidNumber(myinfo.TopImagePos.Length);

        int resourceImgeSize = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage.Length;
            myinfo.TopImagePos[index].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[myinfo.CurrentSubtitleNum];
        myinfo.TopImagePos[index-1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("right", resourceImgeSize, myinfo.CurrentSubtitleNum)];
        myinfo.TopImagePos[index+1].GetComponent<Image>().sprite = ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage[LoopNumber("left", resourceImgeSize, myinfo.CurrentSubtitleNum)];

    }

    // void SetupBottomBarImage(setupBottomImage mydelegate,bool isSetupFront, bool isback, Image[] BackImageOrFrotImage) {
    //    List<Image[]> list = new List<Image[]>();
    //   // Debug.Log(myinfo.BottomTitleText[0].text);
    //    foreach (var item in ReadJson.instance.myinformationList)
    //    {
    //        for (int i = 0; i < myinfo.BottomFrontImage.Length; i++)
    //        {
    //            if (isSetupFront)
    //            {
    //                    mydelegate(i, item, isback, BackImageOrFrotImage);
    //            }
    //            else {
    //                    mydelegate(i, item, isback, BackImageOrFrotImage);
    //            }
    //        } 
    //    }
    //}

    //void setupImage(int i, Information item, bool isback , Image[] BackImageOrFrotImage) {
    //    if (isback)
    //    {
    //        if (item.BigTitle == myinfo.BackBottomTitleSelection[i])
    //        {
    //            BackImageOrFrotImage[i].sprite = item.Bigtitle_BackgroundImagedictionary[myinfo.BackBottomTitleSelection[i]];
    //        }
    //    }
    //    else {
    //        if (item.BigTitle == myinfo.BottomTitleText[i].text)
    //        {
    //            BackImageOrFrotImage[i].sprite = item.Bigtitle_BackgroundImagedictionary[myinfo.tempF[i]];
    //        }
    //    }
       
    //}


    #endregion

    #region TopImageMove

    public void MoveUp(string up) {
        for (int i = 1; i < myinfo.TopImagePos.Length; i++)
        {
            MoveTopImage(i, myinfo.TopimageSlot[i - 1].localPosition, myinfo.TopimageSlot[i - 1].localScale);

        }
        MoveTopImage(0, myinfo.TopimageSlot[4].localPosition, myinfo.TopimageSlot[4].localScale);
        myinfo.TopImagePos = updateArray(up);
        UpdateSubtitle(up);
    }

    public void MoveDown(string down) {
        for (int i = 0; i < myinfo.TopImagePos.Length - 1; i++)
        {
            MoveTopImage(i, myinfo.TopimageSlot[i + 1].localPosition, myinfo.TopimageSlot[i + 1].localScale);

        }
        MoveTopImage(myinfo.TopImagePos.Length - 1, myinfo.TopimageSlot[0].localPosition, myinfo.TopimageSlot[0].localScale);
        myinfo.TopImagePos = updateArray(down);
        UpdateSubtitle(down);
    }

    public void MoveLeft(string left) {
        for (int i = 1; i < myinfo.TopImagePos.Length; i++)
        {
            MoveTopImage(i, myinfo.TopimageSlot[i - 1].localPosition, myinfo.TopimageSlot[i-1].localScale);

        }
        MoveTopImage(0, myinfo.TopimageSlot[4].localPosition, myinfo.TopimageSlot[4].localScale);
        myinfo.TopImagePos = updateArray(left);
        UpdateSubtitle(left);
    }

    public void MoveRight(string Right) {
        for (int i =0; i < myinfo.TopImagePos.Length-1; i++)
        {
                MoveTopImage(i, myinfo.TopimageSlot[i+1].localPosition, myinfo.TopimageSlot[i+1].localScale);

        }
        MoveTopImage(myinfo.TopImagePos.Length - 1, myinfo.TopimageSlot[0].localPosition, myinfo.TopimageSlot[0].localScale);
        myinfo.TopImagePos = updateArray(Right);
        UpdateSubtitle(Right);
    }

    public void LookingForSubContent(int targetSubNumber) {

        if (myinfo.CurrentSubtitleNum != targetSubNumber) {
            MoveRight("right");
            LookingForSubContent(targetSubNumber);
        }           
    }

    void MoveTopImage(int i, Vector3 target, Vector3 scale) {
        LeanTween.moveLocal(myinfo.TopImagePos[i].gameObject, target, .2f);
        LeanTween.scale(myinfo.TopImagePos[i].gameObject, scale, .2f);
    }

    RectTransform[] updateArray(string RightOrLeft) {
        RectTransform[] temp = new RectTransform[myinfo.TopImagePos.Length];
        if (RightOrLeft == "left") {
            for (int i = 0; i < temp.Length-1; i++)
            {
                temp[i] = myinfo.TopImagePos[i + 1];
            }
            temp[temp.Length - 1] = myinfo.TopImagePos[0];
        }else if(RightOrLeft == "right") {
            for (int i = 0; i < temp.Length-1 ; i++)
            {
                temp[i+1] = myinfo.TopImagePos[i];
            }
            temp[0] = myinfo.TopImagePos[temp.Length - 1];
        }
        return temp;
    }


    /*
        void imageInAndOut(string leftAndRight)
        {
            if (leftAndRight == "left") {

            }
        }
        */
    int LoopNumber(string Direction,int size, int currentNum) {

        if (Direction == "left"|| Direction == "up")
        {
            if (currentNum - 1 < 0)
            {
             //   Debug.Log("current number" + (size - 1));
                return currentNum = size - 1;
            }
            else
            {
             //   Debug.Log("current number" + (currentNum - 1));
                return currentNum-1;
            }
        }
        else if (Direction == "right" || Direction == "down") {
            if (currentNum + 1 < size)
            {
              //  Debug.Log("current number" + (0));
                return currentNum + 1;
            }
            else {
           //     Debug.Log("current number" + (currentNum + 1));
                return 0;
            }
        }
        return currentNum;
    
    }

    public void UpdateSubtitle(string Direction) {
            myinfo.CurrentSubtitleNum = LoopNumber(Direction, ReadJson.instance.myinformationList[myinfo.CurrentID].SubImage.Length, myinfo.CurrentSubtitleNum);

    string Highlightbtn = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle[myinfo.CurrentSubtitleNum];
        // Debug.Log("I ma here the important num"+myinfo.CurrentSubtitleNum);
        SetupMidRightBtnHighlightColor(Highlightbtn);
        if (Highlightbtn != "任务表")
        {
            MidInfo[0].SetActive(true);
            MidInfo[1].SetActive(false);
            SetupMidToptext();
            setupMidTopImage();
            string getBaseString = ReadJson.instance.myinformationList[myinfo.CurrentID].SubTitle_MainContentdictionary[Highlightbtn];
            textChangeLine.TextAlignment(getBaseString);
        }
        else {
            MidInfo[0].SetActive(false);
            MidInfo[1].SetActive(true);
          //  SetupMidToptext();
            setupMidTopImage();//为了设置最右边的小圆点高亮
        }

    }


    void SetupMidRightBtnHighlightColor(string HighlightBtn) {
        rightMenuBar.HighlightBtn(HighlightBtn);
    }

    #endregion

    #region general function or math function
    int MidNumber(int arrayLenth)
    {
        return Mathf.FloorToInt(arrayLenth / 2);
    }


    int[] getDifferentNumInRange(int min, int max, int index)
    {
        List<int> temp = new List<int>();
        if (index - 1 <= max - min)
        {
            for (int i = 0; i < index; i++)
            {
                int value = Random.Range(min, max + 1);
                if (i == 0)
                {
                    temp.Add(value);
                }
                else
                {
                    while (temp.Contains(value))
                    {
                        value = Random.Range(min, max + 1);
                    }
                    temp.Add(value);
                }
            }
            int[] differentRandomNum = temp.ToArray();
            temp.Clear();
            return differentRandomNum;
        }
        else
        {
            Debug.Log("Error the index is too big");

        }
        return temp.ToArray();
    }
    #endregion


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // UpdateUI();
       //    UpdateBottomSection();
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string s = eventData.pointerCurrentRaycast.gameObject.name;
       // Debug.Log(s);
        if (s == myinfo.BigTitle.text) {
            DisplayLayer.SetActive(true);

            AVplayOnUGUI.OnAndOffMainBigTitle(false);
            // CanvasManager.instance.SetHideBarTime(60);//设置回到屏保界面时间为60秒；

            canvas.sortingOrder = 2;
            SetxPos(0);
            //AVplayOnUGUI.player[i].transform.localPosition.x = 0;

            this.GetComponent<MouseBehavior>().enabled = true;
        }
    }

    public void SetxPos(float x) {
        this.transform.localPosition = new Vector3(x, this.transform.localPosition.y, this.transform.localPosition.z);
    }

    public void ResetNumber()
    {

       this.GetComponent<Animator>().SetInteger("AnimnationNum", 0);

    }
}
