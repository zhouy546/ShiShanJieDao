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
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    PlayAnimation_1(true);
        //}
        //else if (Input.GetKeyDown(KeyCode.W))
        //{
        //    PlayAnimation_1(false);
        //}
    }

    public void ShowAndHideMainTitle(int AnimationNum , bool b) {

        if (isShowMaintitle != b){
            switch (AnimationNum)
            {
                case 1:
                    PlayAnimation_1(b,AnimationNum);
                    break;

                case 2:
                    StartCoroutine(PlayAnimation_2(b,AnimationNum));
                    break;

                default:
                    break;
            }
        }
    }



    void PlayAnimation_1(bool b,int num) {
        foreach (Animator item in MainTitleAnimatorList)
        {
            item.SetBool("bPlayAnimation1", b);
            item.SetInteger("AnimnationNum", num);
            item.GetComponent<UIClinet>().rightMenuBar.Close("关闭");
        }
    }

    IEnumerator PlayAnimation_2(bool b, int num) {
        foreach (Animator item in MainTitleAnimatorList)
        {
          //  Debug.Log("关闭");
            item.GetComponent<UIClinet>().rightMenuBar.Close("关闭");
        }

        foreach (Animator item in MainTitleAnimatorList)
        {
           // Debug.Log("动画");
            item.SetBool("bPlayAnimation2", b);
            item.SetInteger("AnimnationNum", num);
            yield return new WaitForSeconds(.5f);
        }
    }
}
