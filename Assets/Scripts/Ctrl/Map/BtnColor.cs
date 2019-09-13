using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地图中的按钮信息类
/// </summary>
public class BtnColor :MonoBehaviour
{


    private List<MapOne> mCanBackPtn = new List<MapOne>();
    public List<MapOne> CanBackPtn { get { return mCanBackPtn; } set { mCanBackPtn = value; } }

    private List<MapOne> mHelpPos = new List<MapOne>();
    public List<MapOne> HelpPos { get { return mHelpPos; } set { mHelpPos = value; } }
    //当前路线需要被被点亮的列表  为了熄灭其中和path 不同的列表而存在
    private List<MapOne> mPathActives = new List<MapOne>();
    public List<MapOne> PathActives { get { return mPathActives; } set { mPathActives = value; } }
    //需要被点亮的地面信息
    private List<MapOne> mmaps = new List<MapOne>();
    public List<MapOne> maps { get { return mmaps; } set { mmaps = value; } }
    //当前按钮的路径
    private List<MapOne> mBtnPaths = new List<MapOne>();
    public List<MapOne> BtnPath { get { return mBtnPaths; }set { mBtnPaths=value;} }
    //当前按钮的位置信息
    public Vector2 mPosition { get;private set; }
    //目标按钮的信息类
    public BtnColor mTargetBtnColor { get; set; }

    public GameObject mColorGame { get; private set; }
    public Color mColor { get; set; }


    private bool mIsFinished = false;
    public bool IsFinished { get { return mIsFinished; }set { mIsFinished = value; } }

    public void AddBtnPath(MapOne mapOne)
    {
        if (!mBtnPaths.Contains(mapOne))
        {
            mBtnPaths.Add(mapOne);
        }
    }
    public void AddHelpPath(MapOne mapOne)
    {
        if (!HelpPos.Contains(mapOne))
        {
            HelpPos.Add(mapOne);
        }
    }
    public void AddMapPath(MapOne mapOne)
    {
        if (!mmaps.Contains(mapOne))
        {
            mmaps.Add(mapOne);
        }
    }
    public void AddPathActive(MapOne one)
    {
        if (!mPathActives.Contains(one))
        {
            mPathActives.Add(one);
        }
    }




    private void Start()
    {
        InitColorBtn();
    }
    private void InitColorBtn()
    {
        mPosition = transform.parent.position;
        mColorGame = this.gameObject;
        mColor = GetComponent<SpriteRenderer>().color;
    }
}
