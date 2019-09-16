using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetGoldUI : MonoBehaviour {

    private Button[] dataButton;
    private Button lookBtn;
    void Start()
    {
        dataButton= transform.GetChild(0).Find("DataBtn").GetComponentsInChildren<Button>();
        lookBtn= transform.GetChild(0).Find("Look").GetComponent<Button>();
        InitBtnClick();
    }

    private void InitBtnClick()
    {
        foreach (Button btn in dataButton)
        {
            btn.onClick.AddListener(() =>
            {
                OnDataBtnClick(btn);
            }
            );
        }
    }


    public void OnDataBtnClick(Button btn)
    {
        int btncount = int.Parse(btn.name);
        if (SevenGolf.Instance.CurrentDay == btncount)
        {
            if (btncount == 7)
            {
                GoldSystem.Instance.GetGoldMount(30);

            }
            else
            {
                GoldSystem.Instance.GetGoldMount(5);
            }
            View.Instance.setGetGoldUI(false);
            View.Instance.setGoldEffect(true);
        }
        else
        {
            return;
        }
    }
	void Update ()
    {
		

	}
}
