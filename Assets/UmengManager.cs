/****************************************************
    文件：UmengManager.cs
	作者：SYM    邮箱: 1173973261@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using Umeng;
using UnityEngine;

public class UmengManager : MonoBehaviour 
{
    static string appkey;
    static string temp;

    void Start()
    {
        appkey = "5d810a084ca3573b0b000e23";
        GA.StartWithAppKeyAndChannelId(appkey, "Android");
        GA.ProfileSignIn("playerID");
    }

}