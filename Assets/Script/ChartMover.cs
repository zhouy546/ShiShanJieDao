using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartMover : MonoBehaviour {
    float[] DefaultXpos = { -892.5f,0,892.5f};
    public GameObject[] slots;
    public GameObject[] tempSlot = new GameObject[3];
    // Use this for initialization
    List<int> interger = new List<int>();

    void Start () {
		
	}


    public void MoveSlot(string Direction) {
        //tempSlot = slots;

        if (Direction == "right")
        {
            LeanTween.moveLocalX(slots[0], DefaultXpos[1], .5f);
            LeanTween.moveLocalX(slots[1], DefaultXpos[2], .5f);
            LeanTween.moveLocalX(slots[2], DefaultXpos[0], .5f);
        }
        else if (Direction == "left")
        {
            LeanTween.moveLocalX(slots[0], DefaultXpos[2], .5f);
            LeanTween.moveLocalX(slots[1], DefaultXpos[0], .5f);
            LeanTween.moveLocalX(slots[2], DefaultXpos[1], .5f);
        }


        for (int i = 0; i < slots.Length; i++)
        {
            interger.Add(getInt(i, Direction));
        }
        tempSlot[0] = slots[interger[0]];//1
        tempSlot[1]=slots[interger[1]];//2
        tempSlot[2]=slots[interger[2]];//0

        for (int j = 0; j < tempSlot.Length; j++)
        {
            //Debug.Log(tempSlot[j].name);
         }
        interger.Clear();

        slots[0] = tempSlot[0] ;//1
        slots[1]= tempSlot[1];//2
        slots[2] = tempSlot[2] ;//0

        
    }


    // Update is called once per frame


    float GetTagretPosition(int value,string direction) {
        return DefaultXpos[getInt(value, direction)];

    }


    int getInt(int value,string direction) {

        if (direction == "right")
        {
            if (value + 1 <= 2)
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
                return 2;
            }
        }

        return -1;
    }
}
