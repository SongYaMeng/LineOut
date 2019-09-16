
using UnityEngine;



public class SevenGolf : MonoSingleton<SevenGolf>
{
    // 连续登陆天数
    private const string consecutiveLogInNum = "ConsecutiveLogInNum";
    // 上次登陆的信息
   private const string lastLogIn = "LastLogIn";
    public  int CurrentDay=1;
    private void Start()
    {
        if (CheckLastLogIn() == 1)  // 昨日登陆过，今日首次登陆
        {
            View.Instance.setGetGoldUI(true);
            ConsecutiveDaysAddOne();  // 连续登陆天数加一
        }
        else if (CheckLastLogIn() == 0)   // 今日非首次的连续登陆
        {
            View.Instance.setGetGoldUI(false);
        }
        else if (CheckLastLogIn() == -1)
        {
            View.Instance.setGetGoldUI(true);
            PlayerPrefs.SetInt(consecutiveLogInNum, 0); // 连续登陆天数清零
        }
        // 更新登陆信息
        PlayerPrefs.SetString(lastLogIn, System.DateTime.Now.ToString("yyyy-MM-dd").Replace("-", ""));
    }
    // 判断上次登陆的时间信息
    public int CheckLastLogIn()
    {
        string lastLogInInfo = PlayerPrefs.GetString(lastLogIn, "");  // yyyyMMdd  上次登陆的信息
        int year;
        int month;
        int day;
        int lastLogInInfoNum;
        //
        if (lastLogInInfo.CompareTo("") == 0)
        {
            //没有数据，还没登陆过
            lastLogInInfoNum = -1;
        }
        else
        {
            //有数据，已经登陆过
            year = int.Parse(lastLogInInfo.Remove(4));
            month = int.Parse(lastLogInInfo.Remove(0, 4).Remove(2));
            day = int.Parse(lastLogInInfo.Remove(0, 6));
            System.DateTime last = new System.DateTime(year, month, day);
            System.DateTime today = System.DateTime.Now;
            if ((int)today.DayOfWeek == 0)
            {
                CurrentDay = 7;
            }
            else
            {
                CurrentDay = (int)today.DayOfWeek;
            }
            if (CurrentDay == 1)
            {
                lastLogInInfoNum = -1;
            }
            if ((today - last).Days == 1)    // 昨天登陆过，今天首次登陆
            {
               lastLogInInfoNum = 1;
            }
            else if ((today - last).Days == 0)  // 同一天多次登录，之前没有这个else if，导致每天多次登录后，第二天登录又重新开始的bug
            {
                lastLogInInfoNum = 0;
            }
            else                                   // 登陆间断了
            {
                lastLogInInfoNum = -1;
            }
        }
        return lastLogInInfoNum;
    }
    // 获取连续登陆天数的信息

    public int GetConsecutiveLogInNum()
    {
        return PlayerPrefs.GetInt(consecutiveLogInNum, 0);
    }
    // 连续登陆天数加一
    public void ConsecutiveDaysAddOne()
    {
        int num = PlayerPrefs.GetInt(consecutiveLogInNum, 0);
        PlayerPrefs.SetInt(consecutiveLogInNum, num);
    }

}
