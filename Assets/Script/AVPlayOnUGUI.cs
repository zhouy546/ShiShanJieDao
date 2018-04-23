using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;
//[RequireComponent(typeof(VideoPlayer))]
public class AVPlayOnUGUI : MonoBehaviour
{
    VideoClip videoClip;
    private RenderTexture movie;
    public Image[] image;
    public RawImage[] rawImage;
    public VideoPlayer[] player;
    [SerializeField]
    private UIClinet[] iClinet;
    public UIMode UI;
    public enum UIMode
    {
        None = 0,
        Image = 1,
        RawImage = 2
    }

    public Animator GetAnimator(UIClinet _clinet) {
        Animator animator = _clinet.GetComponent<Animator>();
        return animator;
    }

    public void InitializeMainTitleAnimList() {
     //   Debug.Log(iClinet.Length);
        for (int i = 0; i < iClinet.Length; i++)
        {
           MainTitleAinamtionCtr.instance.MainTitleAnimatorList.Add(GetAnimator(iClinet[i]));
            SetLeftAndRight(i);
        }
    }

    private void SetLeftAndRight(int i) {
        if (i == 0) {
            GetAnimator(iClinet[i]).SetBool("bPlayLeft", true);
        }
        if (i == 1) {
            GetAnimator(iClinet[i]).SetBool("bPlayRight", true);
        }
    }

   public  void SetupGui(int Num) {
        int value = Num*2;
        AssignVideoClip(Num);
        iClinet[0].setupDefalutIDandBigTitle(value);
        iClinet[1].setupDefalutIDandBigTitle(value+1);
    }

    void AssignVideoClip(int Num)
    {
        string s = Num.ToString();
        player[0].clip = GetTheVideoClip(s);
        if (ReadJson.instance.IsPawnAllScreen) {
            player[0].playOnAwake = true;
            player[0].Play();
        }
    }


    VideoClip GetTheVideoClip(string s)
    {
        string path = "Video/" + s;
        videoClip = Resources.Load<VideoClip>(path);

        return videoClip;

    }

    // Use this for initialization
    void Start()
    {
        movie = new RenderTexture(512, 512, 24);
        //player = GetComponent<VideoPlayer>();

        for (int i = 0; i < player.Length; i++)
        {

            if (UI == UIMode.Image)
            {

                // image = GetComponent<Image>();
                player[i].renderMode = VideoRenderMode.RenderTexture;
                player[i].targetTexture = movie;
            }
            else if (UI == UIMode.RawImage)
            {
                //Debug.Log(rawImage[i].gameObject.name);

                //   rawImage = GetComponent<RawImage>();
                player[i].renderMode = VideoRenderMode.APIOnly;

            }
        }

    }
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].isPlaying)
            {
                if (UI == UIMode.Image)
                {
                    //在Image上播放视频
                    if (player[i].targetTexture == null) return;
                    int width = player[i].targetTexture.width;
                    int height = player[i].targetTexture.height;
                    Texture2D t = new Texture2D(width, height, TextureFormat.ARGB32, false);
                    RenderTexture.active = player[i].targetTexture;
                    t.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                    t.Apply();
                    image[i].sprite = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f)) as Sprite;
                }
                if (UI == UIMode.RawImage)
                {
                    //在RawImage上播放视频
                    if (player[i].texture == null) return;
                    rawImage[i].texture = player[i].texture;
                }

            }
        }
    }

    public void OnAndOffMainBigTitle(bool b) {
        for (int i = 0; i < iClinet.Length; i++)
        {
            iClinet[i].MainBigTitlePlane.SetActive(b);
        }
    }
}