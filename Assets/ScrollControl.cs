using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour
{

    /// <summary>
    /// 横向滚动条
    /// </summary>
    public Scrollbar m_HScrollBar;

    /// <summary>
    /// 竖向滚动条
    /// </summary>
    public Scrollbar[] m_VScrollBars;

    /// <summary>
    /// 有竖向滚动的页面
    /// </summary>
    public int[] m_VScrollIndexs;

    /// <summary>
    /// 页面个数
    /// </summary>
    public int m_Num;

    /// <summary>
    /// 设置移动超过多少百分比之后向下翻页
    /// </summary>
    public float m_NextLimit;

    /// <summary>
    /// 滑动敏感值
    /// </summary>
    public float m_Sensitive;

    /// <summary>
    /// 鼠标上一次的位置
    /// </summary>
    private Vector3 mOldPosition;
    /// <summary>
    /// 记录上一次的value
    /// </summary>
    private float mOldValue;
    private float mTargetPosition = 0;
    private int mCurrentIndex = 0;
    private int mTargetIndex = 0;
    /// <summary>
    /// 是否可以移动
    /// </summary>
    private bool mCanMove = false;
    /// <summary>
    /// 初始移动速度
    /// </summary>
    private float mMoveSpeed;
    /// <summary>
    /// 平滑移动参数
    /// </summary>
    private const float SMOOTH_TIME = 0.1f;
    private float mDragParam = 0;
    private float mPageWidth = 0;
    /// <summary>
    /// 是否需要进行滑动方向判定
    /// </summary>
    private bool mNeedCaculate = false;
    /// <summary>
    /// 是否进行竖向滚动
    /// </summary>
    private bool mIsScollV = false;
    /// <summary>
    /// 竖向临时滚动条
    /// </summary>
    private Scrollbar mVScrollBar;
    public void SetNextIndex(int pIndex)
    {
        mTargetIndex = pIndex;
        mTargetPosition = (mTargetIndex - 1) * mPageWidth;
        mIsScollV = false;
        mCanMove = true;
    }
    private void OnPointerDown(Vector2 mousePosition)
    {
        // 记录当前value
        mOldValue = m_HScrollBar.value;
        mOldPosition = Input.mousePosition;
        // mCanMove = false;
        mCurrentIndex = GetCurrentIndex(mOldValue);
        // 判断当前是否在可竖向滑动的页面上
    }
    private void OnDrag(Vector2 mousePosition)
    {
        Vector2 Drag = Input.mousePosition - mOldPosition;

        if (mNeedCaculate)
        {
            mNeedCaculate = false;
        }
        DragScreen(Drag);
        mOldPosition = Input.mousePosition;
    }
    private void OnPointerUp(Vector2 mousePosition)
    {
        Vector2  Drag = Input.mousePosition - mOldPosition;
        DragScreen(Drag);

        mOldPosition = Input.mousePosition;

        float valueOffset = m_HScrollBar.value - mOldValue;
        if (Mathf.Abs((valueOffset) / mPageWidth) > m_NextLimit)
        {
            mTargetIndex += valueOffset > 0 ? 1 : -1;
            mTargetPosition = (mTargetIndex - 1) * mPageWidth;
        }

        mCanMove = true;
    }
    private int GetCurrentIndex(float pCurrentValue)
    {
        return Mathf.RoundToInt(pCurrentValue / mPageWidth + 1);
    }
    private void DragScreen(Vector2 pDragVector)
    {
            float oldValue = m_HScrollBar.value;
            m_HScrollBar.value -= pDragVector.x / Screen.width * mDragParam;
            mMoveSpeed = m_HScrollBar.value - oldValue;
    }
    void Awake()
    {
        mDragParam = 1f / (m_Num - 1) * m_Sensitive;
        mPageWidth = 1f / (m_Num - 1);
        mCurrentIndex = GetCurrentIndex(m_HScrollBar.value);
        mTargetIndex = mCurrentIndex;
    }

    void Start()
    {
        InputControl.Instance.EVENT_MOUSE_DOWN += OnPointerDown;
        InputControl.Instance.EVENT_MOUSE_UP += OnPointerUp;
        InputControl.Instance.EVENT_MOUSE_DRAG += OnDrag;
    }

    void OnDestory()
    {
        InputControl.Instance.EVENT_MOUSE_DOWN -= OnPointerDown;
        InputControl.Instance.EVENT_MOUSE_UP -= OnPointerUp;
        InputControl.Instance.EVENT_MOUSE_DRAG -= OnDrag;
    }


    void Update()
    {
        if (mCanMove)
        {
                if (Mathf.Abs(m_HScrollBar.value - mTargetPosition) < 0.01f)
                {
                    m_HScrollBar.value = mTargetPosition;
                    mCurrentIndex = mTargetIndex;
                    mCanMove = false;
                    return;
                }
                m_HScrollBar.value = Mathf.SmoothDamp(m_HScrollBar.value, mTargetPosition, ref mMoveSpeed, SMOOTH_TIME);

        }
    }
}