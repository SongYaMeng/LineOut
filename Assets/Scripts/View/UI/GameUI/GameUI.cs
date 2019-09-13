using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{


    private Button mSetBtn;
    private Button mReStartlBtn;
    private Button mTipBtn;
    private Text MTipGold;
    private Text mLv;
    private Text mGoldTxt;
    private Text mFillCountTxt;
    private Transform mEniroment;
    private Text mFootCountTxt;
    private Text mMinFootCountTxt;
    private int Count = 0;
    private string[] mFootMin = new string[1119];
    public string[] FootMin { get { return mFootMin; }
        set
        {
            mFootMin = value;
        }
    }

    public void ChangeFootMin(int Index,int FootCount)
    {
        int Min = int.Parse(FootMin[Index]);
        if (Min > FootCount)
        {
            return;
        }
        FootMin[Index] = FootCount.ToString();
        string InitData = string.Empty;
        for (int j = 0; j < 1200; j++)
        {
            if (j == 1119)
            {
                InitData += FootMin[j];
            }
            else
            {
                InitData += FootMin[j] + "-";
            }
        }
        File.WriteAllText(FileLoad.CountDataPath, InitData);
    }

    public string GetLvFoot()
    {
        return FootMin[GetIndex ()- 1];
    }

    public int GetIndex()
    {
        int index = (int)LevelCreateSys.Instance.Diff;
        if (index > 1000)
        {
            index = (index % 100 - 5) * 150;
        }
        else
        {
            index = (index % 10 - 5) * 150;
        }
        index += int.Parse(mLv.text.ToString());
        return index;
    }

    private void Start()
    {
        mEniroment = GameObject.FindWithTag("Eniroment").transform;
        mSetBtn = transform.Find("PannelBtn").Find("SetBtn").GetComponent<Button>();
        mReStartlBtn = transform.Find("PannelBtn").Find("ReStartlBtn").GetComponent<Button>();
        mTipBtn = transform.Find("PannelBtn").Find("TipBtn").GetComponent<Button>();
        MTipGold = mTipBtn.GetComponentInChildren<Text>();
        mLv = transform.Find("Star").Find("Lv").GetComponent<Text >();
        mGoldTxt = transform.Find("Gold").Find("GoldTxt").GetComponent<Text>();
        mFillCountTxt = transform.Find("FillCount").GetChild(0).GetComponent<Text>();
        mFootCountTxt = transform.Find("FootCount").GetChild(0).GetComponent<Text>();
        mMinFootCountTxt = transform.Find("MaxFootCount").GetChild(0).GetComponent<Text>();
        mReStartlBtn.onClick.AddListener(OnRestartStartClick);
        mFootCountTxt.text = "0";
    }
    public void SetFillCountTxt(int count)
    {
        mFillCountTxt.text = count.ToString() + "%";
    }
    public string GetLvTxt()
    {
        return mLv.text;
    }
    public string GetFillCountTxt()
    {
        return mFootCountTxt.text;
    }
    public void SetMinFootCountTxt()
    {
        mMinFootCountTxt.text = GetLvFoot();
    }

    public void InitCount()
    {
        Count = 0;
        mFootCountTxt.text = Count.ToString();
    }
    public void SetFootCountTxt()
    {
        Count++;
        mFootCountTxt.text = Count.ToString();
    }
    public void SetGoldCount()
    {
        mGoldTxt.text = GoldSystem.Instance.GetGoldMount(0).ToString();
    }
    public void OnRestartStartClick()
    {
        OnRestartInit();
        lvOne lv = View.Instance.ChooseLvUI.GetLv(int.Parse(mLv.text));
        InitCount();
        View.Instance.ChooseLvUI.LvBtnClick(lv);
    }
    public void OnRestartInit()
    {
        List<BtnColor> btns = mEniroment.GetComponent<MapCtrl>().mBtnColors;
        
        foreach (BtnColor map in btns)
        {
                if (map == null)
                {
                    mEniroment.GetComponent<MapCtrl>().mBtnColors.Remove(map);
                    break;
               }
                foreach (MapOne one in map.BtnPath) 
                {
                    one.IsObstacle = false;
                    one.GameOne.transform.Find("TrailDrag").GetComponent<BoxCollider2D>().enabled = true;
            }
            map.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void SetmLv(int s)
    {
        mLv.text = s.ToString();
    }
    public int GetmLv()
    {
        return int.Parse(mLv.text);
    }
}
