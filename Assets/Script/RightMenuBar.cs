using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class RightMenuBar : MonoBehaviour, IPointerDownHandler
{

    public TextChangeLine textChangeLine;

    public GameObject BtnPrefab, ClosePrefab,BtnChartPrefab;

    public Color HighlightColor, DefalutColor;

    public UIClinet uIClinet;

    public List<Image> menu = new List<Image>();
    public List<Text> TexSubtitle = new List<Text>();
    public Dictionary<string, Image> subtitleMenuImg = new Dictionary<string, Image>();
    Image PerivousHighlight;
    string[] subtitle;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SetDic());

    }

    IEnumerator SetDic()
    {
        yield return new WaitForSeconds(.5f);
        subtitle = ReadJson.instance.myinformationList[uIClinet.myinfo.CurrentID].SubTitle;
        //generate btn Bar
        GenerateBtn(subtitle);
        TexSubtitle = GetComponentsInChildren<Text>().ToList<Text>();
        
        for (int i = 0; i < subtitle.Length; i++)
        {
            subtitleMenuImg.Add(subtitle[i], menu[i]);
            TexSubtitle[i].text = subtitle[i];
        }

        PerivousHighlight = subtitleMenuImg[subtitle[0]];
        PerivousHighlight.color = HighlightColor;
       // Debug.Log(subtitleMenuImg.Count);
    }

    void GenerateBtn(string[] s) {
        for (int i = 0; i < s.Length; i++)
        {
            InstantiateAbtn(BtnPrefab, s[i], this.transform,true);
         
        }
        InstantiateAbtn(BtnChartPrefab, "chart", this.transform, false);

        InstantiateAbtn(ClosePrefab, "关闭", this.transform,false);
    }


    void InstantiateAbtn(GameObject g, string name, Transform praent, bool addtoMenuList) {
        GameObject _gameObject = Instantiate(g);
        _gameObject.transform.SetParent(praent);
        _gameObject.name = name;
        if (addtoMenuList) {
            menu.Add(_gameObject.GetComponent<Image>());
        }
        _gameObject.transform.localScale = Vector3.one;
        _gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void HighlightBtn(string s)
    {
        try
        {
            PerivousHighlight.color = DefalutColor;
            PerivousHighlight = subtitleMenuImg[s];
            subtitleMenuImg[s].color = HighlightColor;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public void Close(string s) {
            uIClinet.DisplayLayer.SetActive(false);
        uIClinet.AVplayOnUGUI.OnAndOffMainBigTitle(true);
        //for (int i = 0; i < uIClinet.AVplayOnUGUI.player.Length; i++)
        //{
        //    uIClinet.AVplayOnUGUI.player[i].Play();
        //}
       // Debug.Log(uIClinet.DefaultXpos);
        uIClinet.SetxPos(uIClinet.DefaultXpos);
        uIClinet.canvas.sortingOrder = 1;
        uIClinet.GetComponent<MouseBehavior>().enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string s = eventData.pointerCurrentRaycast.gameObject.name;
        if (s == "关闭")
        {
            Close(s);
        } else if (s=="chart") {
            uIClinet.MidInfo[0].SetActive(false);
            uIClinet.MidInfo[1].SetActive(true);
        }
        else {
            uIClinet.MidInfo[0].SetActive(true);
            uIClinet.MidInfo[1].SetActive(false);
            uIClinet.LookingForSubContent(GetTargetNumber(eventData));
            string bigTitle = ReadJson.instance.myinformationList[uIClinet.myinfo.CurrentID].ID_BigTitledictionary[uIClinet.myinfo.CurrentID];
            //Debug.Log(bigTitle);
            // Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            string getBaseString = ReadJson.instance.myinformationList[uIClinet.myinfo.CurrentID].SubTitle_MainContentdictionary[eventData.pointerCurrentRaycast.gameObject.name];

            //      Debug.Log(getBaseString);
            textChangeLine.TextAlignment(getBaseString);
        }
    }

    int GetTargetNumber(PointerEventData eventData) {
        for (int i = 0; i < subtitle.Length; i++)
        {
            if (subtitle[i] == eventData.pointerCurrentRaycast.gameObject.name)
            {
                return i;
            }
        }
        return 0;
    }
}
