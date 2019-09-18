using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Monetization;

public abstract class IadsBase:MonoBehaviour
{
    public virtual void Start()
    {
        Monetization.Initialize(GameId, VideoMode);
    }
    protected string GameId = "3294187";
    protected string placementIdBanner = "Down";
    protected bool BannerMode = true;
    protected string placementIdrewardedVideo = "rewardedVideo";
    protected bool VideoMode = false;
    protected virtual void OnUnityAdsReady(string placementId)
    {
       
    }
    protected virtual void OnUnityAdsDidError(string message)
    {
        
    }
    protected virtual void OnUnityAdsDidStart(string placementId)
    {
     
    }
    protected virtual void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

    }

}