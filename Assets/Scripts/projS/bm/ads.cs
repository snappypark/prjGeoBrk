using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using nj;

public partial class ads : Singleton<ads>
{
    public void Init()
    {
        MobileAds.SetiOSAppPauseOnBackground(true);
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                case AdapterState.NotReady:
                    // The adapter initialization did not complete.
                    MonoBehaviour.print("Adapter: " + className + " not ready.");
                    break;
                case AdapterState.Ready:
                    // The adapter was successfully initialized.
                    MonoBehaviour.print("Adapter: " + className + " is initialized.");
                    break;
                }
            }
            
            if (!ads.Inst.HasBanner())
                ads.Inst.RequestBanner(Test.Active);
        });

    }

    // Returns an ad request with custom ad targeting.
    AdRequest _createAdRequest()
    {
        return new AdRequest.Builder()
          //  .AddTestDevice(AdRequest.TestDeviceSimulator)
         //   .AddTestDevice(_testDeviceUniqId)
            .AddKeyword("game")
            //.SetGender(Gender.Male)
            //.SetBirthday(new DateTime(1985, 2, 29))
            .TagForChildDirectedTreatment(true)
            .AddExtra("max_ad_content_rating", "G")
            .AddExtra("color_bg", "000D29")
            .Build();
    }
}
