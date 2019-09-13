using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 关卡创建系统
/// </summary>
public class LevelCreateSys : MonoSingleton<LevelCreateSys>
{
    //路线精灵
    private List<Sprite> TraillSprite = new List<Sprite>();
    //完成精灵
    private List<Sprite> FinishSprite = new List<Sprite>();
    //未完成精灵
    private List<Sprite> NotFinishSprite = new List<Sprite>();
    //游戏配置文件信息， 第一个int 为难度， 第二个int为level， 第三个为关卡信息
    Dictionary<int, Dictionary<int, string>> Config = new Dictionary<int, Dictionary<int, string>>();
    // 第一个string为按钮 颜色，第二个为按钮位置信息，第三个为 按钮的提升信息
    Dictionary<string, List<int[]>> BtnInfo = new Dictionary<string, List<int[]>>();
    //当前关卡
    private int mLevel;
    public int Level { get { return mLevel; } set { mLevel = value; } }
    //当前难度
    private Difficulty mDiff;
    public Difficulty Diff { get { return mDiff; } set { mDiff = value; } }

    private MapCtrl mapCtrl;
    public static int Row { get; set; }     //行  x轴
    public static int Col   { get; set; }     //列  y轴
    
    public void SetMapSize(int x, int y)
    {
        Row = x;
        Col = y;
    }


    private void LoadSprite()
    {
        for (int i=1;i<16;i++)
        {
            string c = @"Sprites/BtnLine/" + "Btn" + i.ToString() + "Line";
            Sprite sprite =Resources.Load<Sprite>(c);
            TraillSprite.Add(sprite);
        }
    }
    private void LoadNotFinishSprite()
    {
        for (int i = 1; i < 16; i++)
        {
            string c = @"Sprites/Btn/" + "Btn" + i.ToString();
            Sprite sprite = Resources.Load<Sprite>(c);
            NotFinishSprite.Add(sprite);
        }
    }
    private void LoadFinishSprite()
    {
        for (int i = 1; i < 16; i++)
        {
            string c = @"Sprites/BtnFinish/" + "Btn" + i.ToString() + "Finish";
            Sprite sprite = Resources.Load<Sprite>(c);
            FinishSprite.Add(sprite);
        }
    }
    public Sprite GetTraillSprite(string name)
    {
        var m = Regex.Replace(name, @"[^0-9]+", "");
        return TraillSprite[int.Parse(m.ToString())-1];
    }
    public Sprite GetFinishSprite(string name)
    {
        var m = Regex.Replace(name, @"[^0-9]+", "");
        return FinishSprite[int.Parse(m.ToString()) - 1];
    }
    public Sprite GetNotFinishSprite(string name)
    {
        var m = Regex.Replace(name, @"[^0-9]+", "");
        return NotFinishSprite[int.Parse(m.ToString()) - 1];
    }
    private IEnumerator Start()
    {
        LoadSprite();
        LoadFinishSprite();
        LoadNotFinishSprite();
        mapCtrl = GetComponent<MapCtrl>();
        while (FileLoad.data == null)
        {
            yield return new WaitForSeconds(0.1f);
        }  
        Config= LoadFileToDic.Instance.LoadFile(FileLoad.data);
    }


    /// <summary>
    ///  初始化帮助按钮
    /// </summary>
    public void InitTipBtn()
    {
        for (int i = 0; i < mapCtrl.mBtnColors.Count; i += 2)
        {
            mapCtrl.ks.Add(i);
        }
    }

    public void LoadDiffMap(Difficulty difficulty)
    {
        Diff = difficulty;
        int c = LoadFileToDic.Instance.Getdifficulty(difficulty);
        if (c != -1)
        {
            if (c < 100)
            {
                Row = c % 10;
                Col = Row;
            }
            else
            {
                int cc = c % 100;
                Row =cc;
                Col = Row;
            }
        }
        else
        {
            return;
        }
        CreateMap();       
    }
    public void LoadDiffLvBtn(int level)
    {
        BtnInfo= LoadFileToDic.Instance.GetBtnInfo(Diff, level);
        SetColorPos();
    }
    //创建地图资源
    public void CreateMap()
    {
        mapCtrl.Maps.Clear();
        GameObject One = Resources.Load<GameObject>(@"Prefabs\MapOne\MapOne");
        for (int i = 0; i < Row; i++)
        {
            GameObject Map = new GameObject("Map"+i.ToString());
            Map.transform.SetParent(transform);
            Map.transform.localPosition = new Vector3(0,0,0);
            for (int j = 0; j < Col; j++)
            {
                GameObject  go=  Instantiate(One);
                go.transform.position = new Vector3(j,0,0);
                go.transform.name = i.ToString() + "_" + j.ToString();
                go.transform.SetParent(Map.transform);
                go.transform.localPosition = new Vector3(i, j, 0);
                MapOne mapOne = go.GetComponent<MapOne>();
                mapCtrl.AddMaps(mapOne);
            }
        }
    }
    public void SetColorPos()
    {
        mapCtrl.mBtnColors.Clear();
        foreach (string a in BtnInfo.Keys)
        {
            int x1 = BtnInfo[a][0][0];
            int y1 = BtnInfo[a][0][1];
            int x2 = BtnInfo[a][1][0];
            int y2 = BtnInfo[a][1][1];
            BtnColor BtnCol1 = SetBtnInfo(x1, y1,a);
            BtnColor BtnCol2 = SetBtnInfo(x2, y2,a);
            BtnCol1.mTargetBtnColor = BtnCol2;
            BtnCol2.mTargetBtnColor = BtnCol1;
            for (int i = 0; i < BtnInfo[a].Count ; i++)
            {
                BtnCol1.AddHelpPath(mapCtrl.GetMapOne(BtnInfo[a][i][0], BtnInfo[a][i][1]));
            }
        }
    }
    public BtnColor SetBtnInfo(int x1,int y1,string a)
    {
        GameObject go = Resources.Load<GameObject>(@"Prefabs\BtnColor\" + a.ToString()); 
        Transform go1 = transform.GetChild(x1).GetChild(y1);
        go1.GetComponent<SpriteRenderer>().DOFade(0, 0.01f);
        mapCtrl.GetMapOne(x1, y1).IsObstacle = true;
        GameObject go3 = Instantiate(go);
        SetParentInit(go1, go3.transform);
        BtnColor BtnCol1 = go3.transform.GetChild(0).GetComponent<BtnColor>();
        BtnCol1.AddBtnPath(mapCtrl.GetMapOne((int)go1.position.x, (int)go1.position.y));
        BtnCol1.mColor = BtnCol1.GetComponent<SpriteRenderer>().color;
        mapCtrl.AddColors(BtnCol1);
        return BtnCol1;
    }
    public void SetParentInit(Transform Parent,Transform Child)
    {
        Child.SetParent(Parent);
        Child.localPosition = Vector3.zero;
    }


}
