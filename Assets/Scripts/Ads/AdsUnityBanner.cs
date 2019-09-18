/****************************************************
    文件：AdsUnity.cs
	作者：SYM    邮箱: 1173973261@qq.com
    日期：#CreateTime#
	功能：广告
*****************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsUnityBanner : IadsBase 
{
    public override void Start()
    {
        base.Start();
        //Advertisement.Initialize(GameId, BannerMode);
        StartCoroutine(ShowAdBannerReady());
    }


    private IEnumerator ShowAdBannerReady()
    {
        while (!Advertisement.IsReady(placementIdBanner))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementIdBanner);
    }
}