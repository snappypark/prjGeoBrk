using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;
using Beebyte.Obfuscator;
using nj;

public partial class ui_frame : MonoBehaviour
{
    [SerializeField] Image[] _imgSound;
    [SerializeField] AniCurveEx_TranScale _trStage;
    [SerializeField] Text _textStage;
    [SerializeField] Text _textLv;

    [SerializeField] Image _progress;


    public void RefreshStageAndLv()
    {
        RefreshStage();
        RefreshLv();
    }

    public void RefreshStage()
    {
        _textStage.text = LangTable.Stage(User.Inst.CurStageIdx + 1, stageDesign.MaxStageIdx + 1);
        StartCoroutine(_trStage.UpdatePingpong_(1));
    }
    
    public void RefreshLv()
    {
        _textLv.text = string.Format("Lv. {0}", User.Inst.CurLv);
    }

    public void RefreshProgress(float ratio01)
    {
        _progress.fillAmount = ratio01;
    }

    #region UI Action
    int a = 0;
    int b = 0;
    [SkipRename]
    public void OnBtn_Sound()
    {
#if UNITY_EDITOR
        Debug.Log(ambiance.Inst.printColors());
#endif
        a += 11;
        if (UIs.IsWaitingBtn())
            return;
        if (AppAudio.Inst.IsMute)
        {
            AppAudio.Inst.UnMute();
            _imgSound[0].gameObject.SetActive(true);
            _imgSound[1].gameObject.SetActive(false);
        }
        else
        {
            AppAudio.Inst.Mute();
            _imgSound[0].gameObject.SetActive(false);
            _imgSound[1].gameObject.SetActive(true);
        }
    }

    [SkipRename]
    public void OnBtn_tmp()
    {
        if (!Test.Active)
            return;
        b += 13;
        if (a + b == 24)
        {
            UIs.Inst.BackBtn.Show();
            _imgSound[0].transform.localPosition = new Vector3(10, 0);
            _imgSound[1].transform.localPosition = new Vector3(10, 0);
        }
        staging.Inst.ClearObjs();
    }

    #endregion
}
