using System;
using GoogleMobileAds.Api;

public partial class ads/*interstitial*/
{
    const string _TEST_ID_INTER_ = "ca-app-pub-3940256099942544/1033173712";
    const string _REAL_ID_INTER_ = "ca-app-pub-9839048061492395/7635533706";
    
    InterstitialAd _interstitial = null;

    public bool HasInterstitial()
    {
        return _interstitial != null && _interstitial.IsLoaded();
    }

    public void RequestInterstitial(bool isTest)
    {
        string adUnitId = isTest ? _TEST_ID_INTER_ : _REAL_ID_INTER_;

        if (_interstitial != null)
            _interstitial.Destroy();

        // Create an interstitial.
        _interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        _interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        _interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        _interstitial.OnAdOpening += this.HandleInterstitialOpened;
        _interstitial.OnAdClosed += this.HandleInterstitialClosed;
        _interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        _interstitial.LoadAd(this._createAdRequest());
    }
    
    public void ShowInterstitial()
    {
        _interstitial.Show();
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        //       MonoBehaviour.print("HandleInterstitialLoaded event received");
        //   this.interstitial.Show();
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //   MonoBehaviour.print(
        //       "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        // MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        //  MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        //   MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    #endregion
}
