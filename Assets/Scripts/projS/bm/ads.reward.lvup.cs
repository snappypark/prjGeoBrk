using System;
using UnityEngine;
using GoogleMobileAds.Api;

public partial class ads/*reward.lvup*/
{
    const string _TEST_ID_REWARD_ = "ca-app-pub-3940256099942544/5224354917";
    const string _REAL_ID_REWARD_ = "ca-app-pub-9839048061492395/6600131123";

    RewardedAd _rewardedAd = null;

    public bool HasReward() { return _rewardedAd != null && _rewardedAd.IsLoaded(); }

    public void CreateAndLoadRewardedAd(bool isTest)
    {
        if (HasReward())
        {
            UIs.Inst.Menu.BtnAdLvUp.Active(true);
            return;
        }
        string adUnitId = isTest ? _TEST_ID_REWARD_ : _REAL_ID_REWARD_;
        
        _rewardedAd = new RewardedAd(adUnitId);

        _rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        _rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        _rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        _rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        
        AdRequest request = this._createAdRequest();
        _rewardedAd.LoadAd(request);
    }

    public void ShowRewardedAd()
    {
        UIs.Inst.Menu.BtnAdLvUp.Active(false);
        if (HasReward())
            _rewardedAd.Show();
        else
            UIs.Inst.Popup.Show("Rewarded ad is not ready yet.");
    }

    #region RewardedAd callback handlers
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        UIs.Inst.Menu.BtnAdLvUp.Active(true);
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {//MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        UIs.Inst.Popup.Show("sorry.. ads not ready.");
        //UIs.Inst.Popup.Show( string.Format("[error] {0}", args.Message) );
        //MonoBehaviour.print(
        //     "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //UIs.Inst.Popup.Show("ad closed");     
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //MonoBehaviour.print(
        //   "HandleRewardedAdRewarded event received for "
        //                + amount.ToString() + " " + type);
        // Lv up
        hero.Inst.LvUp();
        User.Inst.SaveLv();
    }

    #endregion
}
