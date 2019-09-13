using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDragControll : MonoBehaviour
{

    private Color col;
    private MapCtrl mapCtrl;
    private BtnColor btnColor;
    private ShadowDrag shadow;
    private MapOne Own;

    private void Start()
    {
        mapCtrl = GameObject.Find("Eniroment").GetComponent<MapCtrl>();
        Own = transform.parent.GetComponent<MapOne>();
    }
    private void OnMouseDown()
    {
        if (btnColor == null)
        {
            foreach (BtnColor btn in mapCtrl.mBtnColors)
            {
                if (btn.BtnPath.Contains(Own))
                {                    
                    btnColor = btn;
                }
            }
        }
        if (btnColor != null)
        {
            shadow = btnColor.transform.parent.Find("Shadow").GetComponent<ShadowDrag>();
            Vector3 ShadowCurrentPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x,
            Input.touches[0].position.y, -Camera.main.transform.position.z));
            shadow.transform.position = ShadowCurrentPos;
            btnColor.GetComponent<BtnColorDrag>().SetShadowActive(true);
        }     
    }
    private void OnMouseUp()
    {
        if (btnColor == null) return;
        btnColor.GetComponent<BtnColorDrag>().SetShadowActive();
        shadow = null;
        btnColor = null;
    }
    private void OnMouseDrag()
    {
        if (shadow == null) return;
        Vector3 ShadowCurrentPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x,
           Input.touches[0].position.y, -Camera.main.transform.position.z));
        shadow.transform.position = ShadowCurrentPos;
    }
}
