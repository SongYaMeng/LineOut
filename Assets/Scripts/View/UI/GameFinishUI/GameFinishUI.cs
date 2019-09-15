using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameFinishUI : MonoBehaviour
{
    private MapCtrl mapCtrl;
    private Button mFinishBtn;
    private int mCurrentLv;
    private void Start()
    {
        mapCtrl = GameObject.FindWithTag("Eniroment").GetComponent<MapCtrl>();
        mFinishBtn = GetComponentInChildren<Button>();
        mFinishBtn.onClick.AddListener(OnFinishBtnClick);
    }

    public void SetBtnNotClick(bool b)
    {
        if (mFinishBtn == null)
        {
            return;
        }
        mFinishBtn.enabled = b;
    } 
    public void OnFinishBtnClick()
    {
        Ctrl.Instance.setBlackEffect(false);
        Ctrl.Instance.setBlackEffect(true);
        mFinishBtn.enabled = false;
        int Index=View.Instance.GameUI.GetIndex()-1;
        int Count= int.Parse(View.Instance.GameUI.GetFillCountTxt());
        View.Instance.GameUI.ChangeFootMin(Index, Count);
        StartCoroutine("WaitInit");
    }
    public IEnumerator WaitInit()
    {

       mapCtrl.isClick = false;
        mapCtrl.size = 0;

        yield return new WaitForSeconds(0.2f);
        mFinishBtn.enabled = true;
        mapCtrl.SetInitBtn();
        mapCtrl.isClick = true;
        mCurrentLv = View.Instance.GameUI.GetmLv();
        if (mCurrentLv != 150)
        {
            lvOne lv = View.Instance.ChooseLvUI.GetLv(mCurrentLv + 1);
            View.Instance.ChooseLvUI.LvBtnClick(lv);
            View.Instance.setCancelBtn();
        }
        else
        {
            View.Instance.setChangeDiffuseUI(true);
            View.Instance.setCancelBtn(true);
            mapCtrl.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
