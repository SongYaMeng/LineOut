using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileLoad : MonoBehaviour
{
    [HideInInspector]
    public static string appDBPath;
    [HideInInspector]
    public static string CountDataPath = null;
    [HideInInspector]
    public static string data=null;
    private IEnumerator Start()
    {
        appDBPath = Application.persistentDataPath + "/Playsing.txt";
        CountDataPath= Application.persistentDataPath + "/CountData.txt";
        ReadPlayerFabs();
        ReadFootMinCount();
        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + "/GameLevAndTip.txt";
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/GameLevAndTip.txt";
#elif UNITY_STANDLONE_WIN||UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/GameLevAndTip.txt";
#else
        string.Empty;
#endif
        yield return  StartCoroutine(ReadData(path));
    }

    private  IEnumerator  ReadData(string path)
    {
        WWW www = new WWW(path);
        while (www.isDone==false)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        data = www.text;
    }


    private void ReadPlayerFabs()
    {
        if (!File.Exists(appDBPath))
        {
             File.WriteAllText(appDBPath, "1000-0-0-0-0-0-0-0-0-0");
            GoldSystem.Instance.GetGoldMount(1000);
            View.Instance.GameUI.SetGoldCount();
            View.Instance.PlayUI.SetGoldCount();
            GoldSystem.Instance.AddDatas(55, 0);
            GoldSystem.Instance.AddDatas(66, 0);
            GoldSystem.Instance.AddDatas(77, 0);
            GoldSystem.Instance.AddDatas(88, 0);
            GoldSystem.Instance.AddDatas(99, 0);
            GoldSystem.Instance.AddDatas(1010, 0);
            GoldSystem.Instance.AddDatas(1111, 0);
            GoldSystem.Instance.AddDatas(1212, 0);
            GoldSystem.Instance.AddDatas(1313, 0);
        }
        else
        {
          string a=  File.ReadAllText(appDBPath);
            a.Trim();
           string[] ks= a.Split('-');
            GoldSystem.Instance.AddDatas(55, int.Parse(ks[1]));
            GoldSystem.Instance.AddDatas(66, int.Parse(ks[2]));
            GoldSystem.Instance.AddDatas(77, int.Parse(ks[3]));
            GoldSystem.Instance.AddDatas(88, int.Parse(ks[4]));
            GoldSystem.Instance.AddDatas(99, int.Parse(ks[5]));
            GoldSystem.Instance.AddDatas(1010, int.Parse(ks[6]));
            GoldSystem.Instance.AddDatas(1111, int.Parse(ks[7]));
            GoldSystem.Instance.AddDatas(1212, int.Parse(ks[8]));
            GoldSystem.Instance.GetGoldMount(int.Parse(ks[0]));
           View.Instance.GameUI.SetGoldCount();
            View.Instance.PlayUI.SetGoldCount();
        }
        if (GoldSystem.Instance.GetDatas(88) == 150)
        {
            View.Instance.ChangeDiffuseUI.SetLockDiff(4);
            if (GoldSystem.Instance.GetDatas(99) == 150)
            {
                View.Instance.ChangeDiffuseUI.SetLockDiff(5);
                if (GoldSystem.Instance.GetDatas(1010) == 150)
                {
                    View.Instance.ChangeDiffuseUI.SetLockDiff(6);
                    if (GoldSystem.Instance.GetDatas(1111) == 150)
                    {
                        View.Instance.ChangeDiffuseUI.SetLockDiff(7);
                    }
                }
            }
        }
        View.Instance.ChangeDiffuseUI.SetAllStarTxt();
    }
    private void ReadFootMinCount()
    {
        if (!File.Exists(CountDataPath))
        {
            string InitData = string.Empty;
            for (int j = 1; j <= 1200; j++)
            {
                if (j == 1200)
                {
                    InitData += "0";
                }
                else
                {
                    InitData += "0" + "-";
                }
            }
            File.WriteAllText(CountDataPath, InitData);
        }
        else
        {
            string a = File.ReadAllText(CountDataPath);
            string[] Count = a.Split('-');
            View.Instance.GameUI.FootMin = Count;
        }
    }
}

