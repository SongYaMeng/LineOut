using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening;
public class ScrollbarControll : MonoBehaviour
{
    private Scrollbar _scrollbar1;
    private Scrollbar _scrollbar2;
    private Image _Image;


    public void Start()
    {
        _scrollbar1 = transform.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        _scrollbar2 = transform.Find("ScrollbarTwo").GetComponent<Scrollbar>();
        _Image = _scrollbar2.GetComponent<Image>();
        _scrollbar2.value = _scrollbar1.value;
    }

    public void Update()
    {
        if (_scrollbar2.value != _scrollbar1.value)
        {
            _Image.DOFade(1, 0.9f);
            _scrollbar2.value = _scrollbar1.value;
        }
        else
        {
            _Image.DOFade(0, 0.9f);
        }
    }
}