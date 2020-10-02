using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Admod : MonoBehaviour
{
    public GameObject mAd = null;
    public BannerView bannerView = null;

    // Start is called before the first frame update
    void Start()
    {
        //#if UNITY_ANDROID
        //    string appId = "ca-app-pub-2980262928639137~2499414568";
        //#elif UNITY_IPHONE
        //    string appId = "ca-app-pub-2980262928639137~2499414568";
        //#else
        //    string appId = "unexpected_platform";
        //#endif
        // Initialize the Google Mobile Ads SDK.
        InitializeAds();

        this.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAds()
    {
        if(null == bannerView)
        {
            MobileAds.Initialize(initStatus => { });
        }
    }

    private void RequestBanner()
    {
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2980262928639137/3491580966";
    #elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-2980262928639137/3491580966";
    #else
        string adUnitId = "unexpected_platform";
    #endif

        DestroyAds();

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Leaderboard, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;

        //mAd.AddComponent<BannerView>();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void DestroyAds()
    {
        if(null != bannerView) bannerView.Destroy();
        bannerView = null;
    }

}
