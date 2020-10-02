using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] GameObject LoadingPanel = null;
    [SerializeField] Button AdsButton = null;
    [SerializeField] GameObject AdsNextTime = null;
    [SerializeField] Text strAdsNextTime = null;
    [SerializeField] Text strMyPoint = null;

    string gameId = "3807953";
    string myPlacementId = "rewardedVideo";
    bool testMode = false;
    public int m_nAdsGetPoint = 100;
    bool bLoadAds = false;
    bool bAddPoint = false;
    bool bFirst = true;
    bool bAdsEnable = false;

    // Initialize the Ads listener and service:
    void Start()
    {
        DispPoint();
        if (bFirst)
        {
            bFirst = true;
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
            bAdsEnable = SettingManager.CheckAdsEnable();
            DispNextTime();
        }
    }

    void Update()
    {
        if (AdsButton && bAdsEnable)
        {
            if (bLoadAds && bAddPoint)
            {
                Debug.LogWarning("Point +100 Get.");
                bAddPoint = false;

                // ポイント加算
                SettingManager.SaveGameTotalPoint(m_nAdsGetPoint);
                // 広告表示時間を保存
                SettingManager.SaveAdsTime();
                // チェック
                bAdsEnable = false;

                DispPoint();
            }

            if (Advertisement.IsReady(myPlacementId) && !bLoadAds)
            {
                AdsButton.enabled = true;
            }
            else
            {
                AdsButton.enabled = false;
            }
        }
        else
        {
            DispNextTime();

            if (AdsButton) AdsButton.enabled = false;
        }
    }

    // 次回までの時間表示
    public void DispNextTime()
    {
        if (!AdsNextTime) return;

        bool bDispAdsNextTime = !bAdsEnable;
        AdsNextTime.SetActive(bDispAdsNextTime);

        if (bDispAdsNextTime)
        {
            DateTime PreAdsTime = SettingManager.LoadAdsTime();
            DateTime NowAdsTime = DateTime.Now;
            TimeSpan ts = PreAdsTime.AddDays(1) - NowAdsTime;

            int nSeconds = (int)ts.TotalSeconds;
            strAdsNextTime.text = "次回GETまで\r\n　" + string.Format("{0:00}:{1:00}:{2:00}", nSeconds / 60 / 60, nSeconds / 60 % 60, nSeconds % 60);
        }
    }
    // ポイント表示
    public void DispPoint()
    {
        if (strMyPoint)
        {
            strMyPoint.text = SettingManager.LoadGameTotalPoint() + "pt";
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
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            if(!bAddPoint)
            {
                bAddPoint = true;
                Debug.Log("Ad Finish");
                //SettingManager.SaveGameTotalPoint(m_nAdsGetPoint);
            }
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
