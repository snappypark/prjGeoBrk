using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;
using Beebyte.Obfuscator;
using nj;

public class uiBtnNoAds : MonoBehaviour
{
    [SerializeField] RectTransform _rt;

    public void Refresh()
    {
        bool active = App.Inst.InApp.IsInitialized() && User.Inst.StageAd.IsActive;
        _rt.anchoredPosition = new Vector2(active ? -73 : 200, -60);
    }

    [SkipRename]
    public void OnBtn_Buy()
    {
        if (UIs.IsWaitingBtn(2.7f))
            return;
        App.Inst.InApp.BuyProductID();
    }
}
