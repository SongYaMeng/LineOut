using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderControl : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    public Scrollbar m_Scrollbar;
    public ScrollRect m_ScrollRect;
    public float SmoothValue = 0.1f;
    private float mTargetValue;

    public AnimationCurve _Clip;
    private void Start()
    {
        _Clip.postWrapMode = WrapMode.Once;

    }

    private bool mNeedMove = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        mNeedMove = false;
    }
    public  void OnPointerUp(PointerEventData eventData)
    {
        // 判断当前位于哪个区间，设置自动滑动至的位置
        if (m_Scrollbar.value <= 0.125f)
        {
            mTargetValue = 0;
        }
        else if (m_Scrollbar.value <= 0.375f)
        {
            mTargetValue = 0.25f;
        }
        else if (m_Scrollbar.value <= 0.625f)
        {
            mTargetValue = 0.5f;
        }
        else if (m_Scrollbar.value <= 0.875f)
        {
            mTargetValue = 0.75f;
        }
        else
        {
            mTargetValue = 1f;
        }

        mNeedMove = true;
    }

    public void OnButtonClick(int value)
    {
        switch (value)
        {
            case 1:
                mTargetValue = 0;
                break;
            case 2:
                mTargetValue = 0.25f;
                break;
            case 3:
                mTargetValue = 0.5f;
                break;
            case 4:
                mTargetValue = 0.75f;
                break;
            case 5:
                mTargetValue = 1f;
                break;
            default:
                Debug.LogError("!!!!!");
                break;
        }
        mNeedMove = true;
    }
    float time = 0f;

    void Update()
    {
        if (mNeedMove)
        {          
            if (m_Scrollbar.value < mTargetValue)
            {
                time += Time.deltaTime;
                float value = _Clip.Evaluate(time) * SmoothValue;
                m_Scrollbar.value += value;
                if (m_Scrollbar.value >= mTargetValue)
                {
                    m_Scrollbar.value = mTargetValue;
                    mNeedMove = false;
                    time = 0f;
                }
            }
            else if (m_Scrollbar.value > mTargetValue)
            {
                time += Time.deltaTime;
                float value = _Clip.Evaluate(time) * SmoothValue;
                m_Scrollbar.value -= value;
                if (m_Scrollbar.value <=mTargetValue)
                {
                    m_Scrollbar.value = mTargetValue;
                    mNeedMove = false;
                    time = 0f;
                }
            }
        }
    }

}