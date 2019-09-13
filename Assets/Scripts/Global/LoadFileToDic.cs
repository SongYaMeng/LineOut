using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadFileToDic : MonoSingleton<LoadFileToDic>
{

    //游戏配置文件信息， 第一个int 为难度， 第二个int为level， 第三个为关卡信息
    Dictionary<int, Dictionary<int, string>> config = new Dictionary<int, Dictionary<int, string>>();


    public Dictionary<int, Dictionary<int, string>> LoadFile(string data)
    {
        byte[] Buffers = System.Text.Encoding.UTF8.GetBytes(data);
        ConfigReader configs = new ConfigReader(Buffers);
        config = configs.ReadDictionary();
        return config;
    }

    public int Getdifficulty(Difficulty difficulty)
    {
        int i = 0;
        int c = (int)difficulty;
        foreach (int a in config.Keys)
        {
            if (a == c)
            {
                return a;
            }
            i++;
        }
        return -1;
    }


    public Dictionary<string, List<int[]>> GetBtnInfo(Difficulty difficulty, int Level)
    {

        Dictionary<string, List<int[]>> BtnInfo = new Dictionary<string, List<int[]>>();
        if ((int)difficulty <= 1010)
        {
            foreach (var value in config[(int)difficulty].Keys)
            {
                if (value == Level)
                {
                    string info = config[(int)difficulty][value];
                    info = info.Trim();
                    string[] BtnInfos = info.Split('&');
                    foreach (string One in BtnInfos)
                    {
                        string[] OneInof = One.Split('=');
                        OneInof[1] = OneInof[1].Trim();
                        List<int[]> OnePos = new List<int[]>();
                        for (int i = 0; i < OneInof[1].Length - 1; i += 2)
                        {
                            int in1 = int.Parse(OneInof[1][i].ToString());
                            int in2 = int.Parse(OneInof[1][i+1].ToString());
                            int[] assasa = new int[] { in1, in2 };
                            OnePos.Add(assasa);
                        }
                        BtnInfo.Add(OneInof[0].Trim(), OnePos);
                    }
                    return BtnInfo;
                }
            }
            return null;
        }
        else
        {
            foreach (var value in config[(int)difficulty].Keys)
            {
                if (value == Level)
                {
                    string info = config[(int)difficulty][value];
                    info = info.Trim();
                    string[] BtnInfos = info.Split('&');
                    foreach (string One in BtnInfos)
                    {
                        string[] OneInof = One.Split('=');
                        OneInof[1] = OneInof[1].Trim();
                        string[] vs = OneInof[1].Split('~');
                        List<int[]> OnePos = new List<int[]>();
                        foreach (string c in vs)
                        {
                            int diff = (int)difficulty % 100;
                            int[] ass = GetIndex(int.Parse(c), diff);
                            OnePos.Add(ass);
                        }
                        BtnInfo.Add(OneInof[0].Trim(), OnePos);
                    }
                    return BtnInfo;
                }
            }
            return null;

        }

    }
    public int[] GetIndex(int index, int asdd)
    {
        int k = index / asdd;
        int t = index % asdd;
        int[] vs = new int[] { k, t };
        return vs;
    }

}
public enum Difficulty
{
    Easy = 55,
    Simple = 66,
    Hard = 77,
    Eight = 88,
    Nine = 99,
    Ten = 1010,
    Eve = 1111,
    twele = 1212,
};