using System.Collections.Generic;
using UnityEngine;

public class AStart
{
    public List<MapOne> mPathPosList = new List<MapOne>();

    public List<MapOne> OpenListAll = new List<MapOne>();

    private  MapCtrl mapCtrl;
    #region 单列
    private static AStart instance;
    public static AStart GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new AStart();
            }
            return instance;
        }

    }
    #endregion
    public List<MapOne> GetPath(MapOne startPo, MapOne endPo)
    {
        int count = 0;
        if (mapCtrl == null)
        {
            mapCtrl = GameObject.FindGameObjectWithTag("Eniroment").GetComponent<MapCtrl>();
        }

        if (endPo.mPosition == startPo.mPosition||endPo.IsObstacle==true)
        {
            return null;
        }

        List<MapOne> openList = new List<MapOne>();
        List<MapOne> closeList = new List<MapOne>();
        openList.Add(startPo);
        while (openList.Count > 0)
        {
            MapOne minPo = FindPointWithMinF(openList);
            openList.Remove(minPo);
            closeList.Add(minPo);
            List<MapOne> surroundPoints = FindSurroundPoint(minPo);
            SurroundPointsFilter(surroundPoints, closeList);
            foreach (var surroundPo in surroundPoints)
            {
                if (openList.Contains(surroundPo))
                {
                    continue;
                }
                else
                {
                    surroundPo.ParentCude = minPo;
                    CalcF(surroundPo, endPo);
                    openList.Add(surroundPo);
                    OpenListAll.Add(surroundPo);
                }
            }
            bool b = true;
            foreach (MapOne one in openList)
            {
                b = b && OpenListAll.Contains(one);
            }
            if (b == true)
            {
                count++;
            }
            if (count > 10)
            {
                OpenListAll.Clear();
                return null;
            }
            if (openList.IndexOf(endPo) > -1) { break; }
        }
        return ShowPath(startPo, endPo);
    }

    private List<MapOne> ShowPath(MapOne start, MapOne end)
    {
        mPathPosList.Clear();
        MapOne temp = end;
        if (end.ParentCude == null)
        {
            return null;
        }
        while (true)
        {
            mPathPosList.Add(temp);
            if (temp.ParentCude == start) { mPathPosList.Add(temp.ParentCude); break; }
            temp = temp.ParentCude;
            if (temp == null) break;
            if (mPathPosList.Count > 144)
            {
                mPathPosList.Clear();
                break;
            }
        }
        foreach (MapOne one in mPathPosList)
        {
            one.ParentCude = null;
        }
        return mPathPosList;
    }

    //寻找 Openlist中 F最小的值
    public MapOne FindPointWithMinF(List<MapOne> OpenList)
    {
        float f = float.MaxValue;
        MapOne temp = null;
        foreach (var p in OpenList)
        {
            if (p.F < f)
            {
                temp = p;
                f = p.F;
            }
        }
        return temp;
    }


    //寻找当前点的周围全部点
    public List<MapOne> FindSurroundPoint(MapOne CurPo)
    {
        List<MapOne> list = new List<MapOne>();

        MapOne up = null, down = null, left = null, right = null;     
        if (CurPo.YPosition < LevelCreateSys.Col - 1)
        {
            up = mapCtrl.GetMapOne((int)CurPo.XPosition, (int)CurPo.YPosition + 1);
        }
        if (CurPo.YPosition > 0)
        {
            down = mapCtrl.GetMapOne((int)CurPo.XPosition, (int)CurPo.YPosition - 1);
        }
        if (CurPo.XPosition < LevelCreateSys.Row - 1)
        {
            right = mapCtrl.GetMapOne((int)CurPo.XPosition + 1, (int)CurPo.YPosition);
        }
        if (CurPo.XPosition > 0)
        {
            left = mapCtrl.GetMapOne((int)CurPo.XPosition - 1, (int)CurPo.YPosition);
        }



        if (down != null && down.IsObstacle == false)
        {
            list.Add(down);
        }
        if (up != null && up.IsObstacle == false)
        {
            list.Add(up);
        }
        if (left != null && left.IsObstacle == false)
        {
            list.Add(left);
        }
        if (right != null && right.IsObstacle == false)
        {
            list.Add(right);
        }        
        return list;
    }



    //将关闭带冲你的周围点中移除
    private void SurroundPointsFilter(List<MapOne> surroudPo, List<MapOne> closePo)
    {
        foreach (var closepo in closePo)
        {
            if (surroudPo.Contains(closepo))
            {
                surroudPo.Remove(closepo);
            }
        }
    }
    //计算最小预算值点G值
    private float CalcG(MapOne surround, MapOne minPo)
    {
        return Vector2.Distance(surround.mPosition, minPo.mPosition) + minPo.G;
    }
    //计算该点到终点的F值
    private void CalcF(MapOne now, MapOne end)
    {
        float h = Mathf.Abs(end.XPosition - now.XPosition) + Mathf.Abs(end.YPosition - now.YPosition);
        float g = 0;
        if (now.ParentCude == null)
        {
            g = 0;
        }
        else
        {
            g = Vector2.Distance(new Vector2(now.XPosition, now.YPosition), new Vector2(now.ParentCude.XPosition, now.ParentCude.YPosition)) + now.ParentCude.G;
        }
        float f = g + h;
        now.F = f;
        now.G = g;
        now.H = h;
    }
}
