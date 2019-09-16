using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayUI : MonoBehaviour {

    private Text mGoldTxt;
    private Button mChangeLvBtn;

    private Button mGameActiveBtn;
    private Button mMailBox;
    private Button mTaskBtn;
    private Button mShopBtn;
    private Button mSetBtn;
    private void Start()
    {
      
        mChangeLvBtn = transform.Find("ChangeLvBtn").GetComponent<Button>();
        mGameActiveBtn= transform.Find("ButtomGroup").Find("GameActiveBtn").GetComponent<Button>();
        mMailBox = transform.Find("ButtomGroup").Find("MailBox").GetComponent<Button>();
        mTaskBtn = transform.Find("ButtomGroup").Find("TaskBtn").GetComponent<Button>();
        mShopBtn = transform.Find("ButtomGroup").Find("ShopBtn").GetComponent<Button>();
        mSetBtn = transform.Find("ButtomGroup").Find("SetBtn").GetComponent<Button>();

        mChangeLvBtn.onClick.AddListener(
            delegate () { this.OnChangeLvBtnClick(mChangeLvBtn); }
            );
    }

    public void SetGoldCount()
    {
        if (mGoldTxt == null)
        {
            mGoldTxt = transform.Find("Gold").Find("GoldTxt").GetComponent<Text>();
        }
        mGoldTxt.text = GoldSystem.Instance.GetGoldMount(0).ToString();
    }
    public void OnChangeLvBtnClick(Button button)
    {
        this.gameObject.SetActive(false);
        View.Instance.setChangeDiffuseUI(true);
        View.Instance.setCancelBtn();
    }
}
