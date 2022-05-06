using System.Collections;
using UnityEngine;
using nj;

public class staging : MonoSingleton<staging>
{
    public enum eState
    {
        none = 0,
        play,
        over,
        retry,
        complete,
    }

    stageStats _stats = new stageStats();
    stageWaveMgr _waveMgr = new stageWaveMgr();
    [SerializeField] public waveEditor edit = new waveEditor();
    [SerializeField] public rDots rDots = new rDots();

    eState _state = eState.none;

    public void CheckWin_CountDestroyedObj()
    {
        if (_state != eState.play)
            return;
        if (_stats.goal.isNotFull())
        {
            hero.Inst.GetExp();
            _stats.goal.increase();
            if(_stats.goal.isFull())
                SetStateOnPlay(staging.eState.complete);
            UIs.Inst.Frame.RefreshProgress(_stats.goal.Ratio01());
        }
        else
        {
        }
    }

    public void SetStateOnPlay(eState state)
    {
        if (_state == state)
            return;

        _state = state;
        switch (_state)
        {
            case eState.complete:
                StartCoroutine(UIs.Inst.Playing.Complete_());
                break;
            case eState.over:
                StartCoroutine(UIs.Inst.Playing.Over_());
                break;
            case eState.play:
                StartCoroutine(UIs.Inst.Playing.Contitnue_());
                break;
            case eState.retry:
                StartCoroutine(UIs.Inst.Playing.Retry_());
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case eState.play:
                rDots.Update();
                _waveMgr.Update();
                break;

            case eState.complete:
                break;
        }
#if UNITY_EDITOR
        edit.OnUpdate();
#endif
    }

    public IEnumerator Start_()
    {
        User.Inst.StageAd.OnStageStart();
        yield return _waveMgr.OnStartStage_();
        _state = eState.play;
        _stats.Reset(_waveMgr.totalGoal);
    }
    
    public IEnumerator End_()
    {
        switch (_state)
        {
            case eState.complete:
                User.Inst.Defeated.SetValue(false);
                User.Inst.PreStageIdx.SetValue(User.Inst.CurStageIdx.GetValue());
                User.Inst.CurStageIdx.SetValue(Mathf.Clamp(User.Inst.CurStageIdx + 1, 0, stageDesign.OverStageIdx));
                break;
            default:
                User.Inst.Defeated.SetValue(true);
                //if (User.Inst.PreStageIdx < User.Inst.CurStageIdx)
                //{UIs.Inst.Hud.StageDown.Show(); User.Inst.CurStageIdx.SetValue(Mathf.Clamp(User.Inst.CurStageIdx - 1, 0, stageDesign.OverStageIdx));
                //}
                //User.Inst.CurExp.SetValue(0);
                break;
        }
        User.Inst.StageAd.ShowAds_AfterStage();
        User.Inst.SaveLv();
        yield return null;
        _state = eState.none;
        _waveMgr.OnEndStage();
        ClearObjs();
    }
    
    public void ClearObjs()
    {
        rDots.Clear();
        dots.Inst.UnuseAllGamObj();
        edges.Inst.UnuseAllGamObj();
    }
}
