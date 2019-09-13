using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoSingleton<View>
{
    private Transform mEniroment;
    private Button mCancelBtn;
    private PlayUI mPlayUI;
    public PlayUI  PlayUI { get { return mPlayUI; } }

    private ChangeDiffuseUI mChangeDiffuseUI;
    public ChangeDiffuseUI ChangeDiffuseUI { get { return mChangeDiffuseUI; } }

    private ChooseLvUI mChooseLvUI;
    public ChooseLvUI  ChooseLvUI { get { return mChooseLvUI; } }

    private GameUI mGameUI;
    public GameUI GameUI { get { return mGameUI; } }
    private GameFinishUI mGameFinishUI;
    public GameFinishUI GameFinishUI { get { return mGameFinishUI; } }

    private void Start()
    {
        mEniroment = GameObject.FindWithTag("Eniroment").transform;
        mCancelBtn = transform.GetChild(0).Find("CancelBtn").GetComponent<Button>();
        mCancelBtn.onClick.AddListener(OnCancelClick);
        mPlayUI = transform.GetChild(0).Find("PlayUI").GetComponent<PlayUI>();
        mGameUI = transform.GetChild(0).Find("GameUI").GetComponent<GameUI>();
        mGameFinishUI = transform.GetChild(0).Find("GameFinishUI").GetComponent<GameFinishUI>();
        mChangeDiffuseUI = transform.GetChild(0).Find("ChangeDiffuseUI").GetComponent<ChangeDiffuseUI>();
        mChooseLvUI= transform.GetChild(0).Find("ChooseLvUI").GetComponent<ChooseLvUI>();
        InitUI();
    }

    // 返回点击事件
    public void OnCancelClick()
    {
        if (mChangeDiffuseUI.gameObject.activeSelf)
        {
            setChangeDiffuseUI(false);
            setPlayUI();
            setCancelBtn(false);
        }

        if (mChooseLvUI.gameObject.activeSelf)
        {
            setChooseLvUI(false);
            setChangeDiffuseUI();
        }
        if (mGameUI.gameObject.activeSelf)
        {
            setGameUI(false);
            setChooseLvUI();
            mEniroment.gameObject.SetActive(false);
        }
    }

    #region Active
    public void setGameUI(bool b = true)
    {
        GameUI.gameObject.SetActive(b);
    }
    public void setPlayUI(bool b=true)
    {
        mPlayUI.gameObject.SetActive(b);
    }
    public void setGameFinishUI(bool b = true)
    {
        GameFinishUI.gameObject.SetActive(b);
    }
    public void setChangeDiffuseUI(bool b = true)
    {
        mChangeDiffuseUI.gameObject.SetActive(b);
    }
    public void setChooseLvUI(bool b = true)
    {
        mChooseLvUI.gameObject.SetActive(b);
    }
    public void setCancelBtn(bool b = true)
    {
        mCancelBtn.gameObject.SetActive(b);
    }
    #endregion
    public void InitUI()
    {
        setChangeDiffuseUI(false);
        setChooseLvUI(false);
        setCancelBtn(false);
        setGameUI(false);
        setGameFinishUI(false);
    }


}
