using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartMover : MonoBehaviour {
    float[] DefaultXpos = { -892.5f,0,892.5f};
    public List<GameObject> slots;
    public List<GameObject> tempSlot;    // Use this for initialization
    List<int> interger = new List<int>();

    void Start () {
		
	}


    public void MoveSlot(string Direction) {

        UpdateList(Direction);

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
                tempSlot[getInt(i, Direction)] = slots[i];
            }
        }
        else if (Direction == "left")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                tempSlot[getInt(i, Direction)] = slots[i];
            }
        }

        slots.Clear();
        foreach (GameObject item in tempSlot)
        {
            slots.Add(item);

        }
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
