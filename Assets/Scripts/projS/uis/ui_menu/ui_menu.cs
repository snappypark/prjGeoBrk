using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;
using Beebyte.Obfuscator;
using nj;

public class ui_menu : MonoBehaviour
{
    [SerializeField] public AniCurveEx_UIGraphics context;
    [SerializeField] AniCurveEx_TranScale _tapTextScale;

    [SerializeField] Text _textUninstallLoss;
    [SerializeField] Text _textTap;
    [SerializeField] Text _textVer;

    [SerializeField] public uiBtnAdLvUp BtnAdLvUp;
    [SerializeField] public uiBtnNoAds BtnNoAds;
    
    delay _joystickDeley = new delay(3.21f);

    void OnEnable()
    {
        _joystickDeley.Reset(Test.Active ? 1.21f : 3.21f); //_joystickDeley.Reset(Test.Active ? 3.21f : 3.21f);

        _textUninstallLoss.text = LangTable.UninstallLoss();
        _textVer.text = LangTable.Version(Test.Active);
        _textTap.text = LangTable.MoveJoystic();
        _textUninstallLoss.enabled = false;

        BtnNoAds.Refresh();
    }

    private void OnDisable()
    {
        _textUninstallLoss.enabled = false;
        UIs.Inst.Hud.PlayCnt.Inactive();
    }

    void Update()
    {
        _textUninstallLoss.enabled = UIs.Inst.Frame.IsMoveJoystic;
        _textTap.enabled = !UIs.Inst.Frame.IsMoveJoystic;

        if (UIs.Inst.Frame.IsMoveJoystic)
        {
            if (_joystickDeley.afterOnceTime()
                && App.Inst.FlowMgr.CurType == App.Inst.FlowMgr.NextType)
                    App.Inst.FlowMgr.Change<ProjS.Flow_Play>();
            else
                UIs.Inst.Hud.PlayCnt.Active((int)(_joystickDeley.Ratio10() * 3.99f));
        }
        else
        {
            UIs.Inst.Hud.PlayCnt.Inactive();

            _tapTextScale.Update(0.87f);
            _joystickDeley.Reset();
        }
    }

    #region UI Action

    [SkipRename]
    public void OnBtn_Email()
    {
        if (UIs.IsWaitingBtn())
            return;
        EMail.SentEvent();
    }

    [SkipRename]
    public void OnBtn_Facebook()
    {
        if (UIs.IsWaitingBtn())
            return;
        Application.OpenURL("https://www.facebook.com/line-shot-2351749488441296");
    }

    [SkipRename]
    public void OnBtn_Youtube()
    {
        if (UIs.IsWaitingBtn())
            return;
        Application.OpenURL("https://www.facebook.com/line-shot-2351749488441296");
    }

    [SkipRename]
    public void OnBtn_Rate()
    {
        if (UIs.IsWaitingBtn())
            return;
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.lineshot");
    }

    int a = 0;
    [SkipRename]
    public void OnBtn_WaveEdit()
    {
        a++;
        if (a == 10)
        {
            Test.Active = true;
            UIs.Inst.BackBtn.Show();
        }
#if UNITY_EDITOR
        App.Inst.FlowMgr.Change<ProjS.Flow_EditWave>();
#endif
    }
    
    #endregion
}
