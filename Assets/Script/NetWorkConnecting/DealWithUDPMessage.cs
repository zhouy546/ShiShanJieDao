//*********************❤*********************
// 
// 文件名（File Name）：	DealWithUDPMessage.cs
// 
// 作者（Author）：			LoveNeon
// 
// 创建时间（CreateTime）：	Don't Care
// 
// 说明（Description）：	接受到消息之后会传给我，然后我进行处理
// 
//*********************❤*********************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealWithUDPMessage : MonoBehaviour {
    public enum myenum{ Play,nothing}

    //public FocusByUDP focusByUDPClass;//根据UDP数据处理相机移动的类
    private string dataTest;
    public delegate void foo();
    public static event foo PlayVideo;
    public static event foo EventB;
    public static event foo EventC;
    public void OnEnable()
    {

        PlayVideo += playingVideo;
    }

    public void OnDisable()
    {
        PlayVideo -= playingVideo;

    }
    /// <summary>
    /// 消息处理
    /// </summary>
    /// <param name="_data"></param>
    public void MessageManage(string _data)
    {
        dataTest = _data;
        Debug.Log(dataTest);
        //    focusByUDPClass.CameraFocusON(_data);


        switchcase(Myenum(_data));
    }

    myenum Myenum(string dataTest) {
        if (dataTest == "play" && ReadJson.instance.IsPawnAllScreen==false)
        {
            return myenum.Play;
        }
        return myenum.nothing;
    }

    void switchcase(myenum _myenum) {

        switch (_myenum)
        {
            case myenum.Play:
                doEA();
                break;

            case myenum.nothing:
                break;
            default:
                break;
        }
    }
    
    public void doEA()
    {
        if (PlayVideo != null)
        {
            PlayVideo();
        }
    }


    public void playingVideo() {
       FindObjectOfType<AVPlayOnUGUI>().player[0].Play();
    }
}
