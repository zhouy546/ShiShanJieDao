using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnScreen : MonoBehaviour {
    public GameObject ScreenPrefab;

    List<GameObject> spList = new List<GameObject>();
    public static SpawnScreen instance;
	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        }
	}

    public void SpanwnMyScreen(int Num) {
        if (ReadJson.instance.IsPawnAllScreen)
        {
            int ScreenNum = Num / 2;

            for (int i = 0; i < ScreenNum; i++)
            {
                //Debug.Log("ScreenNum");
                GameObject sp = Instantiate(ScreenPrefab);
                sp.transform.SetParent(this.transform);
                sp.GetComponent<AVPlayOnUGUI>().SetupGui(i);
              //  sp.GetComponent<AVPlayOnUGUI>().InitializeMainTitleAnimList();
                spList.Add(sp);
            }

            for (int i = 0; i < spList.Count; i++)
            {
                spList[i].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                spList[i].GetComponent<RectTransform>().localScale = Vector3.one;
                spList[i].GetComponent<AVPlayOnUGUI>().InitializeMainTitleAnimList();
            }
        }//spaw one
        //else {
        //    GameObject sp = Instantiate(ScreenPrefab);
        //    sp.transform.SetParent(this.transform);
        //    sp.GetComponent<AVPlayOnUGUI>().SetupGui(Num);
        //    sp.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        //    sp.GetComponent<RectTransform>().localScale = Vector3.one;
        //}
        

    }
	
}
