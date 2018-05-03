using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof (Text))]
public class TextChangeLine : MonoBehaviour {
    Text text;
    [SerializeField]
    int NumOfCharEveryLane;


    LinkedList<char> theChar = new LinkedList<char>();
    LinkedList<char> addChar = new LinkedList<char>();
    LinkedList<char> NewChar = new LinkedList<char>();
    void Start() {
        text = this.GetComponent<Text>();
        TextAlignment(text.text);
    }

    public void TextAlignment(string s) {
        string ss = new string(PunctuationCheck(NumOfCharEveryLane, AddStringToTheCharList(s), AddStringToAddCharList()));
        //Debug.Log(PunctuationCheck(NumOfCharEveryLane, AddStringToTheCharList(s), AddStringToAddCharList()).Length);

        text.text = ss;

        NewChar.Clear();
    }

    LinkedList<char> AddStringToTheCharList(string s) {
        string stext;
        theChar.Clear();
        stext = s;//从数据源读取
        char[] _char = stext.ToCharArray();
        for (int i = 0; i < _char.Length; i++)
        {
            theChar.AddLast(_char[i]);
        }
      //  Debug.Log(theChar.Count);
        return theChar;
    }

    LinkedList<char> AddStringToAddCharList() {
        string s = "\n";
        addChar.Clear();
        char[] _Schar = s.ToCharArray();
        for (int i = 0; i < _Schar.Length; i++)
        {
            addChar.AddLast(_Schar[i]);
        }
        return addChar;
    }


    char[] PunctuationCheck(int num, LinkedList<char> _theChar,LinkedList<char> addFirst)
    {
        text.horizontalOverflow = HorizontalWrapMode.Overflow;

        int currentNum = 1;
       
        IEnumerator<char> enumerator = _theChar.GetEnumerator();
       // Debug.Log(enumerator.MoveNext());//删除某些地方会奇怪报错
        LinkedList<char> row = new LinkedList<char>();


        LinkedListNode<char> Node;
        try
        {
            while (enumerator.MoveNext())
            {
                //添加文字到ROW
                row.AddLast(enumerator.Current);
                if (currentNum % num == 0)//换行字数
                {

                    Node = _theChar.Find(enumerator.Current);
                    if (Node != null) {
                        if (Char.IsPunctuation(Node.Next.Value))
                        {//若换行后第一个为标点，缩进标点
                         //添加标点
                            row.AddLast(Node.Next.Value);
                            //添加\N
                            InsertLinkedList(row, addFirst);
                            //检查第一个是否是标点
                            if (Char.IsPunctuation(row.First.Value))
                            {
                                row.RemoveFirst();
                            }

                            //将row添加到NewChar
                            InsertLinkedList(NewChar, row);
                            row.Clear();
                            // Debug.Log(row.Count);

                        }
                        else
                        {
                            InsertLinkedList(row, addFirst);

                            if (Char.IsPunctuation(row.First.Value))
                            {
                                row.RemoveFirst();
                            }
                            InsertLinkedList(NewChar, row);
                            row.Clear();

                        }
                    }
                   
                }

                currentNum++;
            }


            if (Char.IsPunctuation(row.First.Value))
            {
                row.RemoveFirst();
            }

            InsertLinkedList(NewChar, row);
            row.Clear();


            // IEnumerator<char> addenumerator = addFirst.GetEnumerator();


        }
        catch (Exception e)
        {
           Debug.Log(e);
            string ss = new string(_theChar.ToArray());
           // Debug.Log(ss);
            text.text = ss;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;

            return _theChar.ToArray();
        }
        return NewChar.ToArray();

    }


    void InsertLinkedList(LinkedList<char> target, LinkedList<char> mylist) {
        IEnumerator<char> theenumerator = mylist.GetEnumerator();
        while (theenumerator.MoveNext())
        {
            target.AddLast(theenumerator.Current);
        }
    }

}
