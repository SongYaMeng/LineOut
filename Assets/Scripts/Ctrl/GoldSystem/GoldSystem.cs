using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GoldSystem 
{

    private static GoldSystem mInstance;

    public static GoldSystem Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GoldSystem();
            }
            return mInstance;
        }
        private set { }
    }

    private GoldSystem() { }
    private int mGoldInt = 0;
    private Dictionary<int, int> mDatas = new Dictionary<int, int>();
    public Dictionary<int, int> Data { get { return mDatas; } }
    public int GetGoldMount(int Add)
    {
        mGoldInt += Add;
        string k;
        if (mDatas == null||mDatas.Count==0)
        {
             k = mGoldInt.ToString() + "-0-0-0-0-0-0-0-0";
        }
        else
        {
            k = mGoldInt.ToString() + "-" + GetDatas(55).ToString() + "-" + GetDatas(66).ToString() + "-" +
GetDatas(77).ToString() + "-" + GetDatas(88).ToString() + "-" + GetDatas(99).ToString() + "-" +
GetDatas(1010).ToString() + "-" + GetDatas(1111).ToString() + "-" + GetDatas(1212).ToString();
        }
        File.WriteAllText(FileLoad.appDBPath, k);
        return mGoldInt;
    }
    public void AddDatas(int diff,int data)
    {
        if (!mDatas.ContainsKey(diff))
        {
           mDatas.Add(diff, data);
        }
    }
    public int GetDatas(int diff)
    {
        if (mDatas.ContainsKey(diff))
        {
            return mDatas[diff];
        }
        return -1;
    }
    public void SetDatas(int diff, int pass)
    {
        if (mDatas.ContainsKey(diff))
        {
            if (mDatas[diff] <pass)
            {
                mDatas[diff] = pass;
                string k =
         mGoldInt.ToString() + "-" + GetDatas(55).ToString() + "-" + GetDatas(66).ToString() + "-" +
         GetDatas(77).ToString() + "-" + GetDatas(88).ToString() + "-" + GetDatas(99).ToString() + "-" +
         GetDatas(1010).ToString() + "-" + GetDatas(1111).ToString() + "-" + GetDatas(1212).ToString();
                File.WriteAllText(FileLoad.appDBPath, k);
            }
        }
    }
}
