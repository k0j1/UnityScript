using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    //[SerializeField] Button AdsButton = null;
    //[SerializeField] Button TitleButton = null;
    [SerializeField] GameObject AdsButton = null;
    [SerializeField] GameObject TitleButton = null;
    public GameObject hideObj01 = null;
    public GameObject hideObj02 = null;

    string gameId = "3869879";
    string myPlacementId = "rewardedVideo";
    bool testMode = false;
    bool bLoadAds = false;
    bool bFinishAds = false;
    //bool bAddPoint = false;
    bool bFirst = true;
    bool bAdsEnable = true;

    // Initialize the Ads listener and service:
    void Start()
    {
        if (bFirst)
        {
            bFirst = true;
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
    }

    void Update()
    {
        if (bAdsEnable && bFinishAds)
        {
            Debug.LogWarning("Ads OK");
            //TitleButton.enabled = true;
            //AdsButton.enabled = false;
            TitleButton.SetActive(true);
            AdsButton.SetActive(false);
            if (hideObj01) hideObj01.SetActive(false);
            if (hideObj02) hideObj02.SetActive(false);
            bAdsEnable = false;
            bFinishAds = false;
        }
    }

    public void DispAds()
    {
        bLoadAds = ShowRewardedVideo();

        //StartCoroutine("LoadingAds");
    }
    IEnumerator LoadingAds()
    {
        if (!bLoadAds)
        {
            yield return new WaitForSeconds(1); //1秒待つ
            bLoadAds = ShowRewardedVideo();
        }
    }

    public bool ShowRewardedVideo()
    {
        bool bLoad = false;
        //if (LoadingPanel) LoadingPanel.SetActive(false);
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.Show(myPlacementId);
            bLoad = true;
        }
        else
        {
            //if (LoadingPanel) LoadingPanel.SetActive(true);
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
        return bLoad;
    }
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // 動画を見た後は必ずプレイボタンを表示させる
        bFinishAds = true;
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("Ad Finish");
            //bFinishAds = true;
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("OnUnityAdsDidError");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.LogWarning("OnUnityAdsDidStart if=" + placementId);
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Debug.LogWarning("OnDestroy");
    }
}