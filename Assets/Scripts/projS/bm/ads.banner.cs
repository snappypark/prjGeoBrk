using System;
using GoogleMobileAds.Api;

public partial class ads/*banner*/
{
    const string _TEST_ID_BANNER_ = "ca-app-pub-3940256099942544/6300978111";
    const string _REAL_ID_BANNER_ = "ca-app-pub-9839048061492395/6538074455";

    BannerView _bannerView;

    public bool HasBanner()
    {
        return _bannerView != null;
    }
    public void RequestBanner(bool IsTest)
    {
#if UNITY_EDITOR
#elif UNITY_ANDROID
#elif UNITY_IPHONE
#else
#endif
        string adUnitId = IsTest ? _TEST_ID_BANNER_ : _REAL_ID_BANNER_;
        // Clean up banner ad before creating a new one.
        if (_bannerView != null)
            _bannerView.Destroy();

        // Create a 320x50 banner at the top of the screen.
        _bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Register for ad events.
        _bannerView.OnAdLoaded += this.HandleAdLoaded;
        _bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        _bannerView.OnAdOpening += this.HandleAdOpened;
        _bannerView.OnAdClosed += this.HandleAdClosed;
        _bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        _bannerView.LoadAd(this._createAdRequest());
    }

    public void DestoryBanner()
    {
        if (_bannerView != null)
            _bannerView.Destroy();
        UIs.Inst.Ads.NotShowBanner();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        UIs.Inst.Ads.ShowBanner();
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        DestoryBanner();
    }

    #endregion
}
