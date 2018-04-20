using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitleAinamtionCtr : MonoBehaviour {
    public bool isShowMaintitle;
    public static MainTitleAinamtionCtr instance;
   public List<Animator> MainTitleAnimatorList = new List<Animator>();
	// Use this for initialization
	void Start () {
        if (instance == null) {
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayAnimation_1(true);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            PlayAnimation_1(false);
        }
    }

    public void ShowAndHideMainTitle(int AnimationNum , bool b) {

        if (isShowMaintitle != b){
            switch (AnimationNum)
            {
                case 1:
                    PlayAnimation_1(b);
                    break;

                default:
                    break;
            }
        }
    }



    void PlayAnimation_1(bool b) {
        foreach (Animator item in MainTitleAnimatorList)
        {
            item.SetBool("bPlayAnimation1", b);
            item.GetComponent<UIClinet>().rightMenuBar.Close("关闭");
        }
    }
}
