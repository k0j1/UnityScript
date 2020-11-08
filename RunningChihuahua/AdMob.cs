using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMob : MonoBehaviour
{
    public int nAdKind = 0;
    public AdSize mSize = AdSize.Leaderboard;

    private BannerView bannerView;
    readonly string adUnitId = "ca-app-pub-2980262928639137/7697700077";

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }

    public void DestroyAds()
    {
        if (null != bannerView) bannerView.Destroy();
        bannerView = null;
    }

    private void RequestBanner()
    {
        switch(nAdKind)
        {
            case 1:
                mSize = AdSize.Banner;
                break;
            case 2:
                mSize = AdSize.IABBanner;
                break;
            default:
                mSize = AdSize.Leaderboard;
                break;
        }

        this.bannerView = new BannerView(adUnitId, mSize, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }
}
