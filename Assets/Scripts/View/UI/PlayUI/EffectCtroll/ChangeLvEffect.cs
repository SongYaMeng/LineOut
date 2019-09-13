/****************************************************
    文件：ChangeLvEffect.cs
	作者：SYM    邮箱: 1173973261@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLvEffect : MonoBehaviour 
{

    private float Timer=0.2f;
    private List<RectTransform> Rect=new List<RectTransform>();
    private int index=0;
    private int Temp;
    private void Start()
    {
      RectTransform[]  rects = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < rects.Length; i++)
        {
            if (rects[i].name == "ChangeLvBtn") continue;
            rects[i].gameObject.SetActive(false);
            Rect.Add(rects[i]);
        }
    }

    public void Update()
    {
        //Timer -= Time.deltaTime;
        //if (Timer < 0)
        //{
        //    Temp = index;
        //    StartCoroutine("Wait");
        //    index += 1;
        //    Timer = 0.2f;
        //    if (index >= Rect.Count)
        //    {
        //        index = 0;
        //    }
        //}

    }
















}


