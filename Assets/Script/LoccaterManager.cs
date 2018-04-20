using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoccaterManager : MonoBehaviour {
    public GameObject LocaterImage;
    public GameObject upArrow, downArrow;
    public Color HighlightColor, DefalutColor;

    public UIClinet uIClinet;
    public List<Image> menu = new List<Image>();
    public Dictionary<string, Image> subtitleMenuImg = new Dictionary<string, Image>();
    Image PerivousHighlight;
    string[] subtitle;
    // Use this for initialization
    void Start () {
        StartCoroutine(SetDic());
    }

    IEnumerator SetDic()
    {
        yield return new WaitForSeconds(.5f);
        subtitle = ReadJson.instance.myinformationList[uIClinet.myinfo.CurrentID].SubTitle;
        //generate btn Bar
        GenerateBtn(subtitle);

        for (int i = 0; i < subtitle.Length; i++)
        {
            subtitleMenuImg.Add(subtitle[i], menu[i]);
        }

        PerivousHighlight = subtitleMenuImg[subtitle[0]];
        PerivousHighlight.color = HighlightColor;
        // Debug.Log(subtitleMenuImg.Count);
    }


    void GenerateBtn(string[] s)
    {
        InsteantiateArrow(upArrow, this.transform);
        for (int i = 0; i < s.Length; i++)
        {
            InstantiateAbtn(LocaterImage, s[i], this.transform, true);

        }
        InsteantiateArrow(downArrow, this.transform);
    }


    void InstantiateAbtn(GameObject g, string name, Transform praent, bool addtoMenuList)
    {
        GameObject _gameObject = Instantiate(g);
        _gameObject.transform.SetParent(praent);
        _gameObject.name = name;

        if (addtoMenuList)
        {
            menu.Add(_gameObject.GetComponent<Image>());
        }
        _gameObject.transform.localScale = Vector3.one;
        _gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
    }

    void InsteantiateArrow(GameObject g, Transform praent) {
        GameObject _gameObject = Instantiate(g);
        _gameObject.transform.SetParent(praent);
        _gameObject.transform.localScale = Vector3.one;
        _gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
    }

    public void HighlightBtn(string s)
    {
        if (PerivousHighlight != null) {
            PerivousHighlight.color = DefalutColor;
            PerivousHighlight = subtitleMenuImg[s];
            subtitleMenuImg[s].color = HighlightColor;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
