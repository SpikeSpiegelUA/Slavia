using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public static class GoogleAds
{
    public const string banner = "ca-app-pub-9326260447002693/4945401506";
    public const string interpages= "ca-app-pub-9326260447002693/1887993329";
    public static InterstitialAd ad;
    public static BannerView bannerV;
    public static void ShowBanner()
    {
        bannerV = new BannerView(banner, AdSize.SmartBanner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerV.LoadAd(request);
        bannerV.OnAdLoaded += OnBannerLoaded;
    }
    public static void ShowInterpages()
    {        
        ad = new InterstitialAd(interpages);
        AdRequest request = new AdRequest.Builder().Build();
        ad.LoadAd(request);
        ad.OnAdLoaded += OnAdLoaded;
    }
    public static void OnAdLoaded(object sender,System.EventArgs args)
    {
        ad.Show();
    }
    public static void OnBannerLoaded(object sender, System.EventArgs args)
    {
        bannerV.Show();
    }
}
