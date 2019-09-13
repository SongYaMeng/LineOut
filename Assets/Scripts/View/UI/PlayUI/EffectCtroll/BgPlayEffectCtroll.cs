/****************************************************
    文件：BgPlayEffectCtroll.cs
	作者：SYM    邮箱: 1173973261@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using System;
using UnityEngine;
using System.Collections;

public class BgPlayEffectCtroll : MonoBehaviour 
{
    private Animator[] animator;
    private float Timer=1;
    private int index=0;
    private void Start()
    {
        animator= this.GetComponentsInChildren<Animator>();
        foreach (var a in animator)
        {
            a.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            StartCoroutine("Wait");
            index+=1;
            Timer = 1.5f;
            if (index >= 5)
            {
                Timer = 5f;
                index = 0;
            }
        }
    }

    public IEnumerator Wait()
    {
        animator[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        animator[index].gameObject.SetActive(false);
    }
    
}