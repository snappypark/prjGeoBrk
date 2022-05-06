using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using nj;

public partial class User
{
    public class StageAds
    {
        public scBool IsActive = new scBool();
        
        delay _delayReward = new delay(110);
        delay _delayInterstitial = new delay(100);

        public void Load()
        {
            _delayReward.Reset();
            _delayInterstitial.Reset();
            IsActive.SetValue(ObscuredPrefs.GetBool("vit", true));
        }

        public void OnPurchase_NoAds()
        {
            IsActive = false;
            ObscuredPrefs.SetBool("vit", false);
            ObscuredPrefs.Save();
        }

        public void OnStageStart()
        {
            if (Time.time > 90 && _delayInterstitial.IsEnd())
                ads.Inst.RequestInterstitial(Test.Active);
        }

        public void ShowAds_AfterStage()
        {            
            //ad for lvup 
            if (_delayReward.IsEnd() && !ads.Inst.HasReward())
            {
                ads.Inst.CreateAndLoadRewardedAd(Test.Active);
                _delayReward.Reset();
            }

            if (!IsActive)
                return;

            if (!ads.Inst.HasBanner())
                ads.Inst.RequestBanner(Test.Active);

            if (_delayInterstitial.IsEnd())
            {
                if (ads.Inst.HasInterstitial())
                    ads.Inst.ShowInterstitial();
                else
                    UIs.Inst.Ads.ShowInterstitial();
                _delayInterstitial.Reset();
            }
        }
        
        public void edit_reset_z()
        {
            ObscuredPrefs.SetBool("vit", true);
            ObscuredPrefs.SetInt("npl", 0);
        }
    }

}
