using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
public class ChartMover : MonoBehaviour {
    [SerializeField]
    UIClinet uIClinet;

    float[] DefaultXpos = { -892.5f,0,892.5f};
    public List<GameObject> slots;
    public List<GameObject> tempSlot;    // Use this for initialization
    List<int> interger = new List<int>();
    
   
    private List<Sprite> SourceImageList = new List<Sprite>();
    private List<int> num = new List<int>();

    void Start () {


        SourceImageList = ReadJson.instance.myinformationList[uIClinet.myinfo.CurrentID].Chart.ToList();

        num.Add(1);
        num.Add(0);
        num.Add(SourceImageList.Count-1);
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().sprite = SourceImageList[num[i]];
        }

    }


    private void UpdateImage(string Direction)
    {

        for (int i = 0; i < num.Count; i++)
        {
            num[i] = getInt(num[i], SourceImageList.Count, Direction);
            slots[i].GetComponent<Image>().sprite = SourceImageList[num[i]];
            //Debug.Log(num[i]);
        }


        //for (int i = 0; i < SlotImagesList.Count; i++)
        //{
        //    SlotImagesList[i].sprite = SourceImageList[getInt(num, SourceImageList.Count, Direction)];
        //}

    }


    public void MoveSlot(string Direction) {

        UpdateList(Direction);
        UpdateImage(Direction);
        //updateImage

        //move item
        if (Direction == "right")
        {
            slots[2].SetActive(true);
            slots[1].SetActive(true);
            slots[0].SetActive(false);

        }
        else if (Direction == "left") {
            slots[2].SetActive(false);
            slots[1].SetActive(true);
            slots[0].SetActive(true);
        }
        for (int i = 0; i < slots.Count; i++)
        {
            LeanTween.moveLocalX(slots[i], DefaultXpos[i], .5f);
        }
    }

    private void UpdateList(string Direction)
    {
        if (Direction == "right")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                tempSlot[getInt(i, slots.Count, Direction)] = slots[i];
            }
        }
        else if (Direction == "left")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                tempSlot[getInt(i, slots.Count, Direction)] = slots[i];
            }
        }

        slots.Clear();
        foreach (GameObject item in tempSlot)
        {
            slots.Add(item);

        }
    }
    // Update is called once per frame


    //float GetTagretPosition(int value,string direction) {
    //    return DefaultXpos[getInt(value, direction)];

    //}


    int getInt(int value,int length,string direction) {

        if (direction == "right")
        {
            if (value + 1 <= length-1)
            {
                int target = value + 1;
                return target;

            }
            else
            {
                return 0;
            }
        }
        else if(direction == "left"){
            if (value - 1 >= 0)
            {
                int target = value - 1;
                return target;

            }
            else
            {
                return length-1;
            }
        }

        return -1;
    }
}
