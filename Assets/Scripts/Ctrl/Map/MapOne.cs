using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 地图其一
/// </summary>
public class MapOne:MonoBehaviour
{
    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }
    //父格子
    public MapOne ParentCude { get; set; }
    public float XPosition { get; set; }
    public float YPosition { get; set; }
    public bool mIsObstacle = false;
    public bool IsObstacle { get { return mIsObstacle; }set { mIsObstacle = value; } }


    //当前地图是否已经被占用
    public bool misNull { get; set; }
    //当前地图的颜色
    public Color mColor { get; set; }
    //当前地图的位置
    public Vector2 mPosition { get; set; }
    public GameObject GameOne { get; private set; }

    private void Start()
    {
        InitMapOne();
    }

    private void InitMapOne()
    {
        misNull = false;
        XPosition = transform.position.x;
        YPosition = transform.position.y;
        mPosition = new Vector2(XPosition, YPosition);
        GameOne = this.gameObject;
        mColor = this.GetComponent<SpriteRenderer>().color;
    }
}
