using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GoldEffect : MonoBehaviour
{
    private Image[] gold;
    private void Awake()
    {
        gold = transform.GetComponentsInChildren<Image>();
    }
    private void OnEnable()
    {
     
        foreach (var a in gold)
        {
            int x = UnityEngine.Random.Range(-50, 50);
            a.rectTransform.DOLocalMove(new Vector3(x, 50, 0), 0.5f).OnComplete(delegate ()
            {
                a.rectTransform.DOLocalMove(new Vector3(200, 600, 0), 1.5f).OnComplete(delegate () 
                {
                    this.gameObject.SetActive(false);
                    View.Instance.GameUI.SetGoldCount();
                    View.Instance.PlayUI.SetGoldCount();
                    foreach (var a2 in gold)
                    {
                        a2.rectTransform.localPosition = Vector3.zero;
                    }
                }
          );
        });
        }
    }
}
