using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DG.Tweening;

public class ShadowDrag : MonoBehaviour
{
    private MapCtrl mapCtrl;
    //拖尾
    private Vector2 mCurrentPos;
    public Vector2 CurrentPos { get { return mCurrentPos; } set { mCurrentPos = value; } }
    public MapOne isReturn;
    public BtnColor isChange;
    public bool isFinish=false;
    private void Start()
    {
        mapCtrl = GameObject.Find("Eniroment").GetComponent<MapCtrl>();
    }
    int kk = 0;
    int xx = 0;
    private void Update()
    {
        int x = (int)Math.Round(this.transform.position.x);
        int y = (int)Math.Round(this.transform.position.y);
        int x1 = (int)Math.Round(this.transform.position.x) - (int)transform.parent.parent.position.x;
        int y1 = (int)Math.Round(this.transform.position.y) - (int)transform.parent.parent.position.y;
        if (Mathf.Abs(x1) >= 0 || Mathf.Abs(y1) >= 0)
        {
            MapOne TargetOne = mapCtrl.GetMapOne(x, y);
            BtnColor currentBtn = this.transform.parent.GetChild(0).GetComponent<BtnColor>();
            if (x >= LevelCreateSys.Col || y >= LevelCreateSys.Row || x < 0 || y < 0) return;
            if (TargetOne.IsObstacle == false || currentBtn.mTargetBtnColor.mPosition == new Vector2(x, y) || TargetOne.mPosition == currentBtn.mPosition)
            {
                if (TargetOne == currentBtn.BtnPath[currentBtn.BtnPath.Count - 1])
                {
                    return;
                }
                else
                {
                    if (!currentBtn.IsFinished&&!(kk==x1&& xx ==y1))
                    {
                        mapCtrl.source.clip = mapCtrl.mButton;
                        mapCtrl.source.Play();
                        kk = x1;
                        xx = y1;
                    }
                }

                BtnColor TragetBtn = mapCtrl.GetBtnForPath(TargetOne, currentBtn);
                if (TragetBtn != null)
                {
                    TragetBtn.IsFinished = false;
                    TragetBtn.mTargetBtnColor.IsFinished = false;
                    int remove = TragetBtn.BtnPath.IndexOf(TargetOne);
                    TragetBtn.BtnPath.RemoveRange(remove, TragetBtn.BtnPath.Count - remove);
                }
                else
                {
                    mapCtrl.GetHasPathToOther(currentBtn);
                }
                //判断是否可以继续运动，如果按钮的状态没有完成则无法运动
                if (currentBtn.IsFinished)
                {
                    if (!currentBtn.BtnPath.Contains(TargetOne))
                        return;
                    else
                        currentBtn.IsFinished = false;
                    currentBtn.mTargetBtnColor.IsFinished = false;
                }
                //判断 当前路线是否重合，重合整理路线
                if (currentBtn.BtnPath.Contains(TargetOne))
                {
                    if (currentBtn.BtnPath.Count != 1)
                    {
                        int remove = currentBtn.BtnPath.IndexOf(TargetOne);
                        currentBtn.BtnPath.RemoveRange(remove + 1, currentBtn.BtnPath.Count - remove - 1);
                    }
                }
                else
                {
                    currentBtn.AddBtnPath(TargetOne);
                    int Count = currentBtn.BtnPath.Count;
                    float xoff = Math.Abs(currentBtn.BtnPath[Count - 1].XPosition - currentBtn.BtnPath[Count - 2].XPosition);
                    float yoff = Math.Abs(currentBtn.BtnPath[Count - 1].YPosition - currentBtn.BtnPath[Count - 2].YPosition);
                    //判断当前运动的位置是否发生大规模运动
                    if (xoff + yoff != 1)
                    {
                        //ToDo 高速度的响应解决方案  --->
                        currentBtn.BtnPath[currentBtn.BtnPath.Count - 2].GameOne.transform.GetChild(2).gameObject.SetActive(false);
                        List<MapOne> a1 = new List<MapOne>();
                        if (currentBtn.mTargetBtnColor.mPosition == TargetOne.mPosition)
                        {
                            TargetOne.IsObstacle = false;
                            mapCtrl.GetMapOne((int)currentBtn.mPosition.x, (int)currentBtn.mPosition.y).IsObstacle=false;
                            a1 = AStart.GetInstance.GetPath
                            (mapCtrl.GetMapOne((int)currentBtn.BtnPath[Count - 2].XPosition, (int)currentBtn.BtnPath[Count - 2].YPosition),
                           TargetOne);
                            TargetOne.IsObstacle = true;
                        }
                        else
                        {
                            mapCtrl.GetMapOne((int)currentBtn.mPosition.x, (int)currentBtn.mPosition.y).IsObstacle = false;
                            a1 = AStart.GetInstance.GetPath
                            (mapCtrl.GetMapOne((int)currentBtn.BtnPath[Count - 2].XPosition, (int)currentBtn.BtnPath[Count - 2].YPosition),
                           TargetOne);
                        }
                        mapCtrl.GetMapOne((int)currentBtn.mPosition.x, (int)currentBtn.mPosition.y).IsObstacle = true;
                        if (a1 == null || a1.Count == 0) { currentBtn.BtnPath.Remove(TargetOne); return; }
                        else
                        {
                            if (isHas(a1, currentBtn))
                            {
                                int index = IndexMin(a1, currentBtn);
                                currentBtn.BtnPath.RemoveRange(index + 1, currentBtn.BtnPath.Count - index - 1);
                                foreach (MapOne cc in a1)
                                {
                                    int cx = a1.IndexOf(cc);
                                    int insert = a1.IndexOf(currentBtn.BtnPath[index]);
                                    if (cx == 0 || cx == a1.Count - 1) continue;
                                    if (a1.IndexOf(cc) <= insert)
                                    {
                                        int Inde = insert - cx;
                                        currentBtn.BtnPath.Add(a1[Inde]);
                                    }
                                }
                            }
                            else
                            {
                                foreach (MapOne c in a1)
                                {
                                    if (c == a1[a1.Count - 1] || c == a1[0]) continue;
                                    if (c == a1[a1.Count - 1] && c == a1[0]) continue;
                                    int index = a1.Count - a1.IndexOf(c) - 1;
                                    currentBtn.BtnPath.Insert(currentBtn.BtnPath.Count - 1, a1[index]);
                                }
                            }
                        }
                        mapCtrl.GetHasPathToOther(currentBtn);
                    }
                }
                if (currentBtn.mTargetBtnColor.mPosition == new Vector2(x, y))
                {
                    View.Instance.GameUI.SetFootCountTxt();
                    currentBtn.IsFinished = true;
                    currentBtn.mTargetBtnColor.IsFinished = true;
                    if (currentBtn.mColorGame.transform.Find("HelpUI").gameObject.activeSelf == false)
                    {
                        mapCtrl.source.clip = mapCtrl.Winline;
                        mapCtrl.source.Play();
                    }
                }
                if (currentBtn.BtnPath.Count >= 2)
                {
                    MapOne one;
                    for (int i = 0; i < currentBtn.BtnPath.Count-1; i++)
                    {
                        Vector2 ks = currentBtn.BtnPath[i + 1].mPosition - currentBtn.BtnPath[i].mPosition;
                        if (ks.x + ks.y > 1)
                        {
                            one = currentBtn.BtnPath[i+1];
                            currentBtn.BtnPath.RemoveRange(i + 1, currentBtn.BtnPath.Count - i - 1);
                        }
                    }
                }
            }
        }
    }
    public bool isHas(List<MapOne> maps,BtnColor btn)
    {
        foreach (MapOne one in maps)
        {
            if (one == maps[maps.Count - 1]||one==maps[0]) continue;
            if (btn.BtnPath.Contains(one))
            {
                return true;
            }
        }
        return false;
    }
    public int IndexMin(List<MapOne> maps, BtnColor btn)
    {
        int temp = 50;
        foreach (MapOne one in maps)
        {
            if (one == maps[maps.Count - 1]) break;
            if (btn.BtnPath.Contains(one))
            {
                if (temp > btn.BtnPath.IndexOf(one))
                {
                    temp = btn.BtnPath.IndexOf(one);
                }
            }
        }
        return temp;
    }
}