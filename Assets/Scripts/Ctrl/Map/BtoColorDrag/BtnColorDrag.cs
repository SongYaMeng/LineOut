using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BtnColorDrag : MonoBehaviour
{

    private Transform Shadow;
    private MapCtrl mMapCtrl;
    private BtnColor btn;
    private void Start()
    {
        mMapCtrl = this.transform.root.Find("Eniroment").GetComponent<MapCtrl>();
        Shadow = this.transform.parent.Find("Shadow");
        SetShadowActive();
         btn = this.transform.GetComponent<BtnColor>();
    }

    public void SetShadowActive(bool b=false)
    {
        Shadow.gameObject.SetActive(b);
    }




    private void OnMouseDown()
    {
        SetShadowActive(true);
        if (btn.BtnPath.Count != 1)
        {
            btn.BtnPath.RemoveRange(1, btn.BtnPath.Count - 1);
        }
        if (btn.mTargetBtnColor.BtnPath.Count != 1)
        {
            btn.mTargetBtnColor.BtnPath.RemoveRange(1, btn.mTargetBtnColor.BtnPath.Count - 1);
        }
        btn.IsFinished = false;
        btn.mTargetBtnColor.IsFinished = false;
        mMapCtrl.SetBtnColorState();
        transform.DOScale(new Vector3(1.3f, 1.3f, 0), 0.2f).OnComplete(
            ()=>{
                transform.DOScale(new Vector3(1, 1, 0), 0.2f);
                }             
            );
        btn.mTargetBtnColor.mColorGame.transform.DOScale(new Vector3(1.3f, 1.3f, 0), 0.2f).OnComplete(
     () => {
         btn.mTargetBtnColor.mColorGame.transform.DOScale(new Vector3(1, 1, 0), 0.2f);
     }
     );
    }



    private void OnMouseDrag()
    {
        Vector3 ShadowCurrentPos= Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x,
           Input.touches[0].position.y, -Camera.main.transform.position.z));
        Shadow.transform.position = ShadowCurrentPos;
        if (btn.mTargetBtnColor.BtnPath.Count != 1)
        {
            btn.mTargetBtnColor.BtnPath.RemoveRange(1, btn.mTargetBtnColor.BtnPath.Count - 1);
            mMapCtrl.SetBtnColorState();
        }

    }
    private void OnMouseUp()
    {
        SetShadowActive();
        Shadow.transform.localPosition = Vector3.zero;
    }
}
