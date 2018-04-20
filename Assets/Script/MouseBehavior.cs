using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseBehavior : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerDownHandler  {
    //public enum InteractionTypes {
    //    Click, ScrollLeftRight,ScrollUpDown
    //}

    public enum Move
    {
        MoveRight,MoveLeft,Idle
    }

    public UIClinet uiclient;

    struct ClientInfo{
        public Vector2 StartPos;
        public Vector2 UpdatePos;
        public Vector2 EndPos;
        private float Ymovedis;
        public float MoveDistance
        {
            get { return Ymovedis = Mathf.Abs(EndPos.y-StartPos.y); }
        }
    }

    ClientInfo client;

    //public InteractionTypes interactionTypes;

    //public void Do
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData) {
        client.StartPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        client.UpdatePos = eventData.position;
        client.EndPos = eventData.position;
        MoveBehavior(MoveDirection());

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        client.EndPos = eventData.position;

    }

    public void MoveBehavior(Move move) {
        switch (move)
        {
            case Move.MoveRight:
                uiclient.MoveRight("right");
                break;
            case Move.MoveLeft:
                uiclient.MoveLeft("left");
                break;
            case Move.Idle:

                break;
            default:
                break;
        }
    }

    public Move MoveDirection()
    {
        if (xAxisDis() > 0 && client.MoveDistance > 100)
        {
            //Debug.Log("move down");
            client.StartPos = client.EndPos;
            return Move.MoveLeft;
        }
        else if(xAxisDis()<0&&client.MoveDistance > 100) {
            //Debug.Log("move up");
            client.StartPos = client.EndPos;
            return Move.MoveRight;
        }     
        return Move.Idle;
    }

    float xAxisDis() {
        return client.EndPos.y - client.StartPos.y;
    }


    public void OnDrop(PointerEventData eventData) {
       // Debug.Log("OnDrop");
    }


   public void HighlightBtn(string h) {
        //switch (interactionTypes)
        //{
        //    case InteractionTypes.Click:

        //        break;

        //    case InteractionTypes.ScrollLeftRight:
        //        break;

        //    case InteractionTypes.ScrollUpDown:
        //        break;

        //    default:
        //        break;
        //}
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
