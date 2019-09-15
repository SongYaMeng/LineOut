using UnityEngine.UI;


/// <summary>
/// 关卡信息
/// </summary>
public class lvOne
{
    public Button mbutton;
    public int mlv;
    public bool misFinish;

    public lvOne(Button button ,int lv,bool Finish )
    {
        mbutton = button;
        mlv = lv;
        misFinish = Finish;
    }
}

