using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChooseLvUI : MonoBehaviour
{

    private Text mTit;
    private Text mSize;
    public List<lvOne> LvBtns=new List<lvOne>();
    private Transform mEniroment;
    private Sprite SpriteFinish;
    private Sprite SpriteInit;
    private GridLayoutGroup Panel;
    private CanvasScaler _canvasScaler;


    public lvOne GetLv(int lv)
    {
        if(lv>0&&lv<LvBtns.Count)
        return LvBtns[lv-1];
        return null;
    }

    public void Start()
    {
        _canvasScaler = View.Instance.transform.GetChild(0).GetComponent<CanvasScaler>();
        SpriteFinish = Resources.Load<Sprite>(@"Sprites/LvFinish/FinishSprite");
        SpriteInit= Resources.Load<Sprite>(@"Sprites/LvFinish/FinishInit");
        mEniroment = GameObject.FindWithTag("Eniroment").transform;
        mTit = transform.Find("Tit").GetComponent<Text>();
        mSize = transform.Find("Tit").Find("Size").GetComponent<Text>();
        Panel = transform.Find("TopPanel").Find("Panel").GetComponent<GridLayoutGroup>();
        Button[] LvBtn = transform.Find("TopPanel").GetComponentsInChildren<Button>();
        for (int i = 0; i < LvBtn.Length; i++)
        { 
            lvOne lvOne  = new lvOne(LvBtn[i],0,false);
            LvBtns.Add(lvOne);
        }
        InitLvBtn();
        SetInitPanel();
    }


    private void SetInitPanel()
    {

        float currentcanvasHeight = _canvasScaler.referenceResolution.y;
        float xx = (float)Screen.width / Screen.height;
        float currentcanvasWidth = xx * currentcanvasHeight;
        Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(currentcanvasWidth * 5, currentcanvasHeight * 0.66f);
        Panel.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(currentcanvasWidth * 2f,0,0);
        Panel.cellSize = new Vector2(currentcanvasWidth, currentcanvasHeight * 0.66f);
        List<GridLayoutGroup> grids = new List<GridLayoutGroup>();
        GridLayoutGroup[] temps = Panel.GetComponentsInChildren<GridLayoutGroup>();
        for (int i = 0; i < temps.Length; i++)
        {
            if (temps[i] != Panel)
            {
                grids.Add(temps[i]);
            }
        }

        float y = currentcanvasHeight * 0.66f / 6;
        float x = y * 0.88f;
        foreach (GridLayoutGroup a in grids)
        {
            a.cellSize = new Vector2(x, y);
        }
    }


    public void SetInitLv()
    {
        foreach (lvOne one in LvBtns)
        {
            one.mbutton.GetComponent<Image>().sprite = SpriteInit;
        }
    }
    public void SetFinish(int lv)
    {
        GoldSystem.Instance.SetDatas((int)LevelCreateSys.Instance.Diff, lv);
        LvBtns[lv-1].mbutton.GetComponent<Image>().sprite = SpriteFinish;
        View.Instance.ChangeDiffuseUI.SetAllStarTxt();

        if (lv == 150)
        {
            if ((int)LevelCreateSys.Instance.Diff == 88)
            {
                View.Instance.ChangeDiffuseUI.SetLockDiff(4);
            }
           else  if ((int)LevelCreateSys.Instance.Diff == 99)
            {
                View.Instance.ChangeDiffuseUI.SetLockDiff(5);
            }
            else if ((int)LevelCreateSys.Instance.Diff == 1010)
            {
                View.Instance.ChangeDiffuseUI.SetLockDiff(6);
            }
            else if ((int)LevelCreateSys.Instance.Diff == 1111)
            {
                View.Instance.ChangeDiffuseUI.SetLockDiff(7);
            }
        }
        View.Instance.ChangeDiffuseUI.SetAllStarTxt();
    }
    public void setTitText(string s)
    {
        mTit.text = s;
    }
    public void setSizeText(string s)
    {
        mSize.text = s;
    }
    private void InitLvBtn()
    {
        int i = 0;
        foreach (lvOne one in LvBtns)
        {
            one.mbutton.GetComponentInChildren<Text>().text = (i + 1).ToString();
            one.mlv = i + 1;
            one.mbutton.onClick.AddListener(delegate () { this.LvBtnClick(one); });
            i++;
        }
    }
    public void LvBtnClick(lvOne one)
    {
        if (one.mlv != 1)
        {
            Sprite sprite = LvBtns[one.mlv - 2].mbutton.GetComponent<Image>().sprite;
            if (sprite != SpriteFinish)
            {
                return;
            }
        }    
        View.Instance.GameUI.OnRestartInit();
        mEniroment.gameObject.SetActive(true);
        mEniroment.GetComponent<MapCtrl>().InitLoadLv();
        mEniroment.GetComponent<MapCtrl>().InitLoadBtn();
        mEniroment.GetComponent<MapCtrl>().SetBtnColorState();
        LevelCreateSys.Instance.LoadDiffLvBtn(one.mlv);
        mEniroment.GetComponent<MapCtrl>().ks.Clear();
        LevelCreateSys.Instance.InitTipBtn();
        this.gameObject.SetActive(false);
        View.Instance.setGameUI();
        View.Instance.GameUI.SetmLv(one.mlv);
        View.Instance.GameUI.SetMinFootCountTxt();
        int aaa = (int)LevelCreateSys.Instance.Diff;
        float size=0f;
        float Aspect = (float)Screen.width / Screen.height;
        if (Aspect >= 0.5625)
        {
            size = 4.5f;
        }
        else
        {
            size = 5.1f;
        }

        if (aaa == 55)
        {
            Camera.main.orthographicSize = size;
            Camera.main.transform.position = new Vector3(2, 1.5f, -10);
        }
        else if (aaa == 66)
        {
            Camera.main.orthographicSize = size+1;
            Camera.main.transform.position = new Vector3(2.5f, 2, -10);
        }
        if (aaa == 77)
        {
            Camera.main.orthographicSize = size + 2;
            Camera.main.transform.position = new Vector3(3, 2.5f, -10);
        }
        else if (aaa == 88)
        {
            Camera.main.orthographicSize = size + 3;
            Camera.main.transform.position = new Vector3(3.5f, 3, -10);
        }
        if (aaa == 99)
        {
            Camera.main.orthographicSize = size + 4;
            Camera.main.transform.position = new Vector3(4, 3.5f, -10);
        }
        else if (aaa == 1010)
        {
            Camera.main.orthographicSize = size + 5;
            Camera.main.transform.position = new Vector3(4.5f, 4f, -10);
        }
        if (aaa == 1111)
        {
            Camera.main.orthographicSize = size + 6;
            Camera.main.transform.position = new Vector3(5, 4.5f, -10);
        }
        else if (aaa == 1212)
        {
            Camera.main.orthographicSize = size + 7;
            Camera.main.transform.position = new Vector3(5.5f, 5f, -10);
        }
       else if (aaa == 1313)
        {
            Camera.main.orthographicSize = size + 8;
            Camera.main.transform.position = new Vector3(6, 5.5f, -10);
        }
        View.Instance.GameUI.InitCount();
    }
    public void SetHasPassLv()
    {
        int diff=(int)LevelCreateSys.Instance.Diff;
        int Pass = GoldSystem.Instance.GetDatas(diff);
        if (Pass == -1) return;
        for (int i=1;i<=Pass;i++)
        {
            SetFinish(i);
        }
    }
}
