using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdsUnityViedo:IadsBase
{

    public void ShowAd()
    {
        Monetization.Initialize(GameId, VideoMode);
        StartCoroutine(ShowAdWhenReady());
    }
    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(placementIdrewardedVideo))
        {
            yield return new WaitForSeconds(0.25f);
        }
        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementIdrewardedVideo) as ShowAdPlacementContent;
        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }
    public void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            GoldSystem.Instance.GetGoldMount(20);
            View.Instance.GameUI.SetAdsActive(false);
            View.Instance.setGoldEffect();
        }
        else if (result == ShowResult.Skipped)
        {
          
        }
        else if (result == ShowResult.Failed)
        {
            
        }
    }

}