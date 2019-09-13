using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class ChangeDiffuseUI : MonoBehaviour {


    private Text mStarAllTxt;
    private Button[] mBasicPack;
    private  Transform mEniroment;

    void Start ()
    {
        mEniroment = GameObject.FindWithTag("Eniroment").transform;
        mStarAllTxt = transform.Find("StarAll").GetComponentInChildren<Text>();
        AddBasicPackClick();
    }

    private void Awake()
    {
        mBasicPack = transform.Find("Scroll View").GetComponentsInChildren<Button>();
    }
    public void SetLockDiff(int index)
    {
        mBasicPack[index].transform.GetChild(3).gameObject.SetActive(false);
        mBasicPack[index].transform.GetChild(2).gameObject.SetActive(true);
    }

    public void AddBasicPackClick()
    {
        foreach(Button button in mBasicPack)
        {
            button.onClick.AddListener(delegate(){ this.BasicPackClick(button); });
        }
    }

    public void SetStarTxt(int index,int passMount)
    {

        if (mBasicPack[index].transform.Find("LoclImg").gameObject.activeSelf==true)
        {
            return;
        }
        Text text= mBasicPack[index].transform.Find("StarBg").Find("Star").GetComponentInChildren<Text>();
        text.text = passMount.ToString()+"/150";
    }


    public void SetAllStarTxt()
    {   
        int i = 0;
        foreach (var a in GoldSystem.Instance.Data.Values)
        {        
            SetStarTxt(i++, a);
        }
    }



    public void BasicPackClick(Button button)
    {


        if (button.transform.Find("LoclImg").gameObject.activeSelf == true)
        {
            return;
        }

        int childCount = mEniroment.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform game = mEniroment.GetChild(i);
            Destroy(game.gameObject);
        }
        string result = Regex.Replace(button.name, @"[^0-9]+", "");
        int k= int.Parse(result.ToString());
        Difficulty diff = (Difficulty)k;
        LevelCreateSys.Instance.Diff=diff;
        LevelCreateSys.Instance.LoadDiffMap(diff);
        int passLv = GoldSystem.Instance.GetDatas((int)LevelCreateSys.Instance.Diff);
        View.Instance.ChooseLvUI.SetInitLv();
        if (passLv != 0)
        {
            while (passLv != 0)
            {
                View.Instance.ChooseLvUI.SetFinish(passLv);
                passLv--;
            }
        }
        mEniroment.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        View.Instance.ChooseLvUI.setTitText(button.transform.Find("Text").GetComponent<Text>().text);
        View.Instance.ChooseLvUI.setSizeText(button.transform.Find("Text").Find("Text (1)").GetComponent<Text>().text);
        View.Instance.ChooseLvUI.SetHasPassLv();
        View.Instance.setChooseLvUI();
    }

}
