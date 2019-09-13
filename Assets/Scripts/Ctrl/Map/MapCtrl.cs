using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 管理  地图信息
/// </summary>
public class MapCtrl : MonoBehaviour
{
    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public AudioClip mFinish;
    [HideInInspector]
    public AudioClip mButton;
    [HideInInspector]
    public AudioClip AutoLine;
    [HideInInspector]
    public AudioClip Winline;
    private LevelCreateSys levelSys;
    //地图信息列表
    private List<MapOne> mMaps = new List<MapOne>();
    public List<MapOne> Maps { get { return mMaps; } }
    //地图按钮信息列表
    private List<BtnColor> BtnColors = new List<BtnColor>();
    public List<BtnColor> mBtnColors { get { return BtnColors; } }
    //地图颜色信息列表
    public MapOne GetMapOne(int x, int y)
    {
        int off = x * (LevelCreateSys.Col) + y;
        if (off >= 0 && off < (LevelCreateSys.Col * LevelCreateSys.Row))
        {
            return mMaps[off];
        }

        return null;
    }
    public BtnColor GetBtnForPath(MapOne mapOne, BtnColor Btn)
    {

        foreach (BtnColor btn in BtnColors)
        {
            if (btn.BtnPath.Contains(mapOne) && Btn != btn && Btn.mTargetBtnColor != btn)
            {
                return btn;
            }
        }
        return null;
    }
    public BtnColor GetBtnForpos(MapOne one)
    {
        foreach (BtnColor btn in BtnColors)
        {

            if (btn.mPosition == one.mPosition)
            {
                return btn;
            }
        }
        return null;
    }
    public void GetHasPathToOther(BtnColor btn)
    {
        if (btn == null) return;
        BtnColor kes = null;
        for (int i = 0; i < BtnColors.Count; i++)
        {
            if (btn != BtnColors[i] && btn.mTargetBtnColor != BtnColors[i])
            {
                for (int j = 0; j < btn.BtnPath.Count; j++)
                {
                    kes = GetBtnForPath(btn.BtnPath[j], btn);
                    if (kes != null)
                    {
                        int remove = kes.BtnPath.IndexOf(btn.BtnPath[j]);
                        kes.BtnPath.RemoveRange(remove, kes.BtnPath.Count - remove);
                        kes.IsFinished = false;
                        kes.mTargetBtnColor.IsFinished = false;
                    }
                }
            }
        }
    }
    //按照颜色得到地图得到按钮信息 
    public List<BtnColor> GetBtnColors(Color color)
    {
        List<BtnColor> btnColors = new List<BtnColor>();
        foreach (BtnColor col in BtnColors)
        {
            if (col.mColor == color)
            {
                btnColors.Add(col);
            }
        }
        return btnColors;
    }
    public void AddMaps(MapOne one)
    {
        if (mMaps.Contains(one))
        {
            return;
        }
        mMaps.Add(one);
    }
    public void AddColors(BtnColor btn)
    {
        if (BtnColors.Contains(btn))
        {
            return;
        }
        BtnColors.Add(btn);
    }
    public void RemoveColors(BtnColor btn)
    {
        if (BtnColors.Contains(btn))
        {
            BtnColors.Remove(btn);
        }
    }
    public BtnColor GetBtnColors(int x, int y)
    {
        foreach (BtnColor btn in BtnColors)
        {
            if (btn.mPosition.x == x && btn.mPosition.y == y)
            {
                return btn;
            }
        }
        return null;
    }
    private int size = 0;
    private bool b = true;

    private bool isFull()
    {
        bool c = true;
        float i = 0;
        foreach (MapOne one in mMaps)
        {
            if (one.GameOne.transform.GetChild(2).gameObject.activeSelf)
            {
                i++;
            }
            c = c && one.GameOne.transform.GetChild(2).gameObject.activeSelf;
        }
        i = (i / Maps.Count) * 100;
        View.Instance.GameUI.SetFillCountTxt((int)i);
        return c;
    }
    
    public  bool isClick=true;
    public void CheckFinish()
    {
        foreach (BtnColor btn in BtnColors)
        {
            if (btn == null) return;
            Transform sprite = btn.transform.Find("BtnEffect");
            if (btn.IsFinished)
            {
                sprite.gameObject.SetActive(true);
                btn.GetComponent<SpriteRenderer>().sprite = levelSys.GetFinishSprite(btn.name);
            }
            else 
            {
                sprite.gameObject.SetActive(false);
                btn.GetComponent<SpriteRenderer>().sprite = levelSys.GetNotFinishSprite(btn.name);
            }
        }
    }
    //public void Set
    private void SetRotation()
    {
        foreach (BtnColor btn in BtnColors)
        {         
            if (btn == null) return;
            if (btn.BtnPath.Count >= 2)
            {
                Vector2 Dir = btn.BtnPath[1].mPosition - btn.mPosition;
                GameObject mgo = btn.mColorGame.transform.parent.parent.Find("Trail").gameObject;
                if (Dir.x == 1)
                {
                    mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
                    btn.transform.eulerAngles = new Vector3(0, 0, 90);
                    mgo.transform.localPosition = new Vector3(0.6f, 0, 0);
                    mgo.transform.localEulerAngles = new Vector3(0, 0, 0);
                    mgo.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    mgo.SetActive(true);
                }
                else if (Dir.x == -1)
                {
                    mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
                    btn.transform.eulerAngles = new Vector3(0, 0, -90);
                    mgo.transform.localPosition = new Vector3(-0.6f, 0, 0);
                    mgo.transform.localEulerAngles = new Vector3(0, 0, 0);
                    mgo.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    mgo.SetActive(true);
                }
                else if (Dir.y == 1)
                {
                    mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
                    btn.transform.eulerAngles = new Vector3(0, 0, 180);
                    mgo.transform.localPosition = new Vector3(0, 0.6f, 0);
                    mgo.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    mgo.transform.localEulerAngles = new Vector3(0, 0, 90);
                    mgo.SetActive(true);
                }
                else if (Dir.y == -1)
                {
                    mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
                    btn.transform.eulerAngles = new Vector3(0, 0, 0);
                    mgo.transform.localPosition = new Vector3(0, -0.6f, 0);
                    mgo.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    mgo.transform.localEulerAngles = new Vector3(0, 0, 90);
                    mgo.SetActive(true);
                }
                if (btn.IsFinished)
                {
                    int Count = btn.BtnPath.Count;
                    Vector3 Dir2 = btn.BtnPath[Count - 2].mPosition - btn.BtnPath[Count - 1].mPosition;
                    GameObject mgo1 = btn.BtnPath[Count - 2].transform.Find("Trail").gameObject;
                    if (Dir2.x == 1)
                    {
                        btn.mTargetBtnColor.transform.eulerAngles = new Vector3(0, 0, 90);
                        mgo1.transform.localPosition = new Vector3(-0.4f, 0, 0);
                        mgo1.transform.localEulerAngles = new Vector3(0, 0, 0);
                        mgo1.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    }
                    else if (Dir2.x == -1)
                    {
                        btn.mTargetBtnColor.transform.eulerAngles = new Vector3(0, 0, -90);
                        mgo1.transform.localPosition = new Vector3(0.4f, 0, 0);
                        mgo1.transform.localEulerAngles = new Vector3(0, 0, 0);
                        mgo1.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                    }
                    else if (Dir2.y == 1)
                    {
                        btn.mTargetBtnColor.transform.eulerAngles = new Vector3(0, 0, 180);
                        mgo1.transform.localPosition = new Vector3(0, -0.4f, 0);
                        mgo1.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                        mgo1.transform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                    else if (Dir2.y == -1)
                    {
                        btn.mTargetBtnColor.transform.eulerAngles = new Vector3(0, 0, 0);
                        mgo1.transform.localPosition = new Vector3(0, 0.4f, 0);
                        mgo1.transform.localScale = new Vector3(3.35f, 0.7f, 1);
                        mgo1.transform.localEulerAngles = new Vector3(0, 0, 90);
                    }
                }
            }
            if (btn.IsFinished == false && btn.BtnPath.Count < 2)
            {
                btn.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public void Update()
    {
        CheckFinish();
        SetBtnColorState();
        SetRotation();
        if (isFull()&&IsFinishLv()&&isClick)
        {
            foreach (BtnColor btn in BtnColors)
            {
                btn.GetComponent<BoxCollider2D>().enabled = false;
                btn.mTargetBtnColor.GetComponent<BoxCollider2D>().enabled = false;
                foreach (MapOne one in btn.BtnPath)
                {
                    one.GameOne.transform.Find("TrailDrag").GetComponent<BoxCollider2D>().enabled = false;
                    one.IsObstacle = true;
                }
            }
            Camera.main.orthographicSize += 0.02f;
            View.Instance.setGameFinishUI();
            View.Instance.setGameUI(false);
            View.Instance.setCancelBtn(false);
            int mCurrentLv = View.Instance.GameUI.GetmLv();
            View.Instance.ChooseLvUI.SetFinish(mCurrentLv);
            if (size == 0)
            {
                View.Instance.GameFinishUI.SetBtnNotClick(true);
                source.clip = mFinish;
                source.Play();
                int aaa = (int)LevelCreateSys.Instance.Diff;
                if (aaa == 55)
                {
                    size = 7;
                }
                else if(aaa==66)
                {
                    size = 8;
                }
                else if (aaa == 77)
                {
                    size = 9;
                }
                else if (aaa == 88)
                {
                    size = 10;
                }
                else if (aaa == 99)
                {
                    size = 11;
                }
                else if (aaa == 1010)
                {
                    size = 12;
                }
                else if (aaa == 1111)
                {
                    size = 13;
                }
                else if (aaa == 1212)
                {
                    size = 14;
                }
                else if (aaa == 1313)
                {
                    size = 15;
                }

            }
            if (Camera.main.orthographicSize >= size)
            {
                isClick = false;
                size = 0;
            }

        }

    }
    public  void SetInitBtn()
    {
        foreach (BtnColor btn in BtnColors)
        {
            btn.IsFinished = false;
        }
    }
    private void Start()
    {
        levelSys = GetComponent<LevelCreateSys>();
        source = GetComponent<AudioSource>();
        mFinish = Resources.Load<AudioClip>("Sound/Win");
        mButton= Resources.Load<AudioClip>("Sound/Button");
        AutoLine = Resources.Load<AudioClip>("Sound/AutoLine");
        Winline = Resources.Load<AudioClip>("Sound/Winline");
        source.clip = mFinish;
    }
    //初始化关卡信息
    public void InitLoadLv()
    {

        foreach (BtnColor btn in BtnColors)
        {
            btn.BtnPath.RemoveRange(1, btn.BtnPath.Count - 1);
        }
    }
    //初始化按钮信息
    public void InitLoadBtn()
    {
        if (BtnColors == null || BtnColors.Count == 0) return;
        foreach (BtnColor btn in BtnColors)
        {
            if (btn == null) return;
            btn.mColorGame.transform.parent.parent.GetComponent<SpriteRenderer>().DOFade(1, 0.01f);
           MapOne one =  GetMapOne((int)btn.mPosition.x, (int)btn.mPosition.y);
            one.transform.Find("BgMapOne").gameObject.SetActive(false);
            Destroy(btn.mColorGame.transform.parent.gameObject);
        }
    }

    #region 判断是否过关

    private bool IsFinishLv()
    {
        bool b = true;
        if (BtnColors.Count == 0 || BtnColors == null) return false;
        foreach (BtnColor btn in BtnColors)
        {
            b = b && btn.IsFinished;
        }
        return b;
    }

    #endregion

    #region 同步状态信息
    public void SetBtnColorState()
    {

        foreach (BtnColor btn in BtnColors)
        {
            if (btn.BtnPath == null)
            {
                btn.BtnPath = new List<MapOne>();
                btn.AddBtnPath(GetMapOne((int)btn.mPosition.x, (int)btn.mPosition.y));
            }
            if (btn.BtnPath.Count != 1)
            {
                SetBgPathToMaps(btn);
                for (int i = 0; i < btn.BtnPath.Count-1; i++)
                {
                    
                    btn.BtnPath[i].mColor = btn.mColor;
                    Vector2 Dir = btn.BtnPath[i + 1].mPosition - btn.BtnPath[i].mPosition;
                    if (i==0) continue;                     
                    SetBtnPathToMapone(Dir, btn.BtnPath[i], btn);                   
                }
            }
            SetNeedNoActives(btn);
            SetNeedNoBgActive(btn);
        }
    }
    #endregion


    #region 设置活跃的状态
    private void SetBgPathToMaps(BtnColor btn)
    {
        Color col = btn.mColorGame.transform.parent.Find("Shadow").GetComponent<SpriteRenderer>().color;
        foreach (MapOne map in btn.BtnPath)
        {
            GameObject go = map.GameOne.transform.Find("BgMapOne").gameObject;
            go.SetActive(true);
            go.GetComponent<SpriteRenderer>().color = col;
            btn.AddMapPath(map);
        }
    }
    private void SetBtnPathToMapone(Vector2 Dir, MapOne go, BtnColor btn)
    {
        GameObject mgo = go.GameOne.transform.Find("Trail").gameObject;     
        if (Dir.x == 1)
        {   
            mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
            mgo.transform.localPosition = new Vector3(0.5f, 0f, 0);
            mgo.transform.localEulerAngles = new Vector3(0, 0, 0);
            mgo.transform.localScale = new Vector3(4.45f, 0.7f, 1);
            mgo.SetActive(true);
            if (btn.PathActives.Count == 0)
            {
                btn.AddPathActive(btn.BtnPath[0]);
            }
            btn.AddPathActive(go);
        }
        else if (Dir.x == -1)
        {
            mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
            mgo.transform.localPosition = new Vector3(-0.5f, 0, 0);
            mgo.transform.localScale = new Vector3(4.45f, 0.7f, 1);
            mgo.transform.localEulerAngles = new Vector3(0, 0, 0);
            mgo.SetActive(true);
            if (btn.PathActives.Count == 0)
            {
                btn.AddPathActive(btn.BtnPath[0]);
            }
            btn.AddPathActive(go);
        }
        else if (Dir.y == 1)
        {
            mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
            mgo.transform.localPosition = new Vector3(0, 0.5f, 0);
            mgo.transform.localScale = new Vector3(4.45f, 0.7f, 1);
            mgo.transform.localEulerAngles = new Vector3(0, 0, 90);
            mgo.SetActive(true);
            if (btn.PathActives.Count == 0)
            {
                btn.AddPathActive(btn.BtnPath[0]);
            }
            btn.AddPathActive(go);
        }
        else if (Dir.y == -1)
        {
            mgo.GetComponent<SpriteRenderer>().sprite = levelSys.GetTraillSprite(btn.name);
            mgo.transform.localPosition = new Vector3(0, -0.5f, 0);
            mgo.transform.localScale = new Vector3(4.45f, 0.7f, 1);
            mgo.transform.localEulerAngles = new Vector3(0, 0, 90);
            mgo.SetActive(true);
            if (btn.PathActives.Count == 0)
            {
                btn.AddPathActive(btn.BtnPath[0]);
            }
            btn.AddPathActive(go);
        }
    }
    #endregion

    #region 设置不活跃的状态
    private void SetNeedNoActives(BtnColor btn)
    {
        if (btn == null) return;
        if (btn.PathActives.Count == 0) return;
        if (btn.BtnPath.Count == 1)
        {
            for (int i = 0; i < btn.PathActives.Count; i++)
            {              
                btn.PathActives[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
                btn.PathActives[i].GameOne.transform.Find("BgMapOne").gameObject.SetActive(false);
            }
            btn.PathActives.Clear();
        }
        if ((btn.BtnPath.Count - 1) != btn.PathActives.Count)
        {
            for (int i=0;i<btn.PathActives.Count;i++)
            {
                if (!btn.BtnPath.Contains(btn.PathActives[i]))
                {
                 btn.PathActives[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
                }
            }           
            int order = btn.PathActives.IndexOf(btn.BtnPath[btn.BtnPath.Count - 2]);
            if (order != btn.PathActives.Count - 1)
            {
                for (int i = order + 1; i < btn.PathActives.Count; i++)
                {
                    btn.PathActives[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
                }
                btn.PathActives.RemoveRange(order + 1, btn.PathActives.Count - order - 1);
            }
        }
    }
    private void SetNeedNoBgActive(BtnColor btn)
    {
        if (btn == null) return;
        if (btn.maps.Count == 0) return;
        if (btn.BtnPath.Count == 1)
        {
            for (int i = 0; i < btn.maps.Count; i++)
            {
                btn.maps[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
                btn.maps[i].GameOne.transform.Find("BgMapOne").gameObject.SetActive(false);
            }
            btn.maps.Clear();
        }
        else
        {
            List<MapOne> need = GetNeedToNoActive(btn.maps,btn.BtnPath);
            if (need != null)
            {
                foreach (MapOne map in need)
                {
                    map.GameOne.transform.Find("BgMapOne").gameObject.SetActive(false);
                }
            }
        }

    }
    public void SetAllNOActives(BtnColor btn)
    {
        for (int i = 0; i < btn.PathActives.Count; i++)
        {
            btn.PathActives[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
            btn.PathActives[i].GameOne.transform.Find("BgMapOne").gameObject.SetActive(false);
        }
        btn.PathActives.Clear();
        for (int i = 0; i < btn.maps.Count; i++)
        {
            btn.maps[i].GameOne.transform.Find("Trail").gameObject.SetActive(false);
            btn.maps[i].GameOne.transform.Find("BgMapOne").gameObject.SetActive(false);
        }
        btn.maps.Clear();

    }
    #endregion
    private List<MapOne> GetNeedToNoActive(List<MapOne> maps,List<MapOne> btnPath)
    {
        List<MapOne> Need=new List<MapOne>();
        for (int i=0;i<maps.Count;i++)
        {
            if (!btnPath.Contains(maps[i]))
            {
                Need.Add(maps[i]);
                maps.RemoveAt(i);
            }
        }
        for (int j=0;j<btnPath.Count;j++)
        {
            if (!maps.Contains(btnPath[j]))
            {
                maps.Add(btnPath[j]);
            }
        }
        return Need;
    }

    //提示信息功能
    public List<int> ks = new List<int>();
    public void SetTipPaht()
    {
        if (GoldSystem.Instance.GetGoldMount(0) < 5)
        {
            return;
        }
        GoldSystem.Instance.GetGoldMount(-5);
        View.Instance.GameUI.SetGoldCount();
        View.Instance.PlayUI.SetGoldCount();
        int rodom = UnityEngine.Random.Range(0,ks.Count);
        if (ks.Count == 0) return;
        BtnColor btn = BtnColors[ks[rodom]];
        ks.Remove(ks[rodom]);
        MapOne mapOne2 = btn.HelpPos[1];
        btn.HelpPos.RemoveAt(1);
        btn.AddHelpPath(mapOne2);
        if (btn.IsFinished)
        {
            bool b = true;
            List<MapOne> isTrue = new List<MapOne>();
            isTrue.Add(GetMapOne((int)btn.mPosition.x,(int)btn.mPosition.y));
            for (int i = 0; i < btn.HelpPos.Count - 1; i++)
            {
                Vector2 Path = btn.HelpPos[i + 1].mPosition - btn.HelpPos[i].mPosition;
                MapOne one = new MapOne();
                one.XPosition = -1;
                one.YPosition = -1;
                if (Path.x != 0)
                {
                    int d = (int)btn.HelpPos[i].XPosition;
                    while (one.XPosition != btn.HelpPos[i + 1].XPosition)
                    {
                        d += (int)(Path.x / Mathf.Abs(Path.x));
                        one = GetMapOne(d, (int)btn.HelpPos[i].YPosition);
                        isTrue.Add(one);
                    }
                }
                else
                {
                    int c = (int)btn.HelpPos[i].YPosition;
                    while (one.YPosition != btn.HelpPos[i + 1].YPosition)
                    {
                        c += (int)(Path.y / Mathf.Abs(Path.y));
                        one = GetMapOne((int)btn.HelpPos[i].XPosition, c);
                        isTrue.Add(one);
                    }
                }
            }
            if (isTrue.Count == btn.BtnPath.Count||isTrue.Count==btn.mTargetBtnColor.BtnPath.Count)
            {
                if (isTrue.Count == btn.BtnPath.Count)
                {
                    for (int i = 0; i < isTrue.Count; i++)
                    {
                        b = b && btn.BtnPath.Contains(isTrue[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < isTrue.Count; i++)
                    {
                        b = b &&btn.mTargetBtnColor.BtnPath.Contains(isTrue[i]);
                    }
                }
            }
            else
            {
                b = false;
            }
            if (b)
            {
                isTrue.Clear();
                SetTipPaht();
                return;
            }
            b = true;
        }
        btn.PathActives.Clear();
        SetAllNOActives(btn);
        if (btn.BtnPath.Count != 1)
        {
            btn.BtnPath.RemoveRange(1, btn.BtnPath.Count - 1);
        }
        if (btn.mTargetBtnColor.BtnPath.Count != 1)
        {
            btn.mTargetBtnColor.BtnPath.RemoveRange(1, btn.mTargetBtnColor.BtnPath.Count - 1);
        }
        for (int i = 0; i < btn.HelpPos.Count - 1; i++)
        {
            Vector2 Path = btn.HelpPos[i + 1].mPosition - btn.HelpPos[i].mPosition;
            MapOne one = new MapOne();
            one.XPosition = -1;
            one.YPosition = -1;
            if (Path.x != 0)
            {
                int d = (int)btn.HelpPos[i].XPosition;
                while (one.XPosition != btn.HelpPos[i + 1].XPosition)
                {
                    d += (int)(Path.x / Mathf.Abs(Path.x));
                    one = GetMapOne(d, (int)btn.HelpPos[i].YPosition);
                    btn.AddBtnPath(one);
                }
            }
            else
            {
                int c = (int)btn.HelpPos[i].YPosition;
                while (one.YPosition != btn.HelpPos[i + 1].YPosition)
                {
                    c += (int)(Path.y / Mathf.Abs(Path.y));
                    one = GetMapOne((int)btn.HelpPos[i].XPosition, c);
                    btn.AddBtnPath(one);
                }
            }
        }
        btn.GetComponent<BoxCollider2D>().enabled = false;
        btn.mTargetBtnColor.GetComponent<BoxCollider2D>().enabled = false;
        GetHasPathToOther(btn);
        btn.IsFinished = true;
        btn.transform.Find("HelpUI").gameObject.SetActive(true);
        btn.mTargetBtnColor.IsFinished = true;
        btn.mTargetBtnColor.transform.Find("HelpUI").gameObject.SetActive(true);
        btn.maps = btn.BtnPath;
        foreach (MapOne one in btn.BtnPath)
        {         
            one.GameOne.transform.Find("TrailDrag").GetComponent<BoxCollider2D>().enabled = false;
            one.IsObstacle = true;
            if (btn.BtnPath[btn.BtnPath.Count - 1] != one)
            {
                btn.AddPathActive(one);
            }
        }
        source.clip = AutoLine;
        source.Play();
        View.Instance.GameUI.SetFootCountTxt();
    }

}
