using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoSingleton<Ctrl>
{
    private GameObject mBlackEffect;
    public GameObject BlackEffect { get { return mBlackEffect; } }



    void Start () {
        mBlackEffect = transform.Find("BlackEffect").gameObject;
        setBlackEffect(false);
    }


    public void setBlackEffect(bool b = true)
    {
        mBlackEffect.gameObject.SetActive(b);
    }
}
