using System.Collections;
using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;

using Beebyte.Obfuscator;
using nj;

public class ui_playing : MonoBehaviour
{
    [SerializeField] AniCurveEx_GameObj_UIGraphics _completed;
    [SerializeField] AniCurveEx_GameObj_UIGraphics _overed;

    [SerializeField] Text _edgeNum;

    public void RefreshEdgeNum()
    {
    //    _edgeNum.text = string.Format("{0}", edges.Inst.NumObj);
    }

    public IEnumerator Complete_()
    {
        _completed.contexts.SetAlpha(0.0f);
        _completed.rootGameObj.SetActive(true);
        effs.Inst.Create(effs.typeStageComplemented, new Vector3(3.2f, 17.5f));
        yield return null; yield return null;
        effs.Inst.Create(effs.typeStageComplemented, new Vector3(12.5f, 14.5f));

        yield return _completed.contexts.FadeIn_();
        yield return new WaitForSeconds(1.0f);
        yield return Retry_();
    }

    public IEnumerator Over_()
    {
        _overed.contexts.SetAlpha(0.0f);
        _overed.rootGameObj.SetActive(true);
        yield return _overed.contexts.FadeIn_();
        yield return new WaitForSeconds(1.0f);
        yield return Retry_();
    }

    public IEnumerator Contitnue_()
    {
        Time.timeScale = 1.0f;
        yield return _overed.contexts.FadeOut_();
        _overed.rootGameObj.SetActive(false);
        hero.Inst.Init();
    }

    public IEnumerator Retry_()
    {
        UIs.Inst.Frame.RefreshProgress(0.0f);
        Time.timeScale = 1.0f;
        yield return _overed.contexts.FadeOut_();

        _completed.rootGameObj.SetActive(false);
        _overed.rootGameObj.SetActive(false);
        App.Inst.FlowMgr.Change<ProjS.Flow_Menu>();
    }

    #region UI ACTION
    [SkipRename]
    public void OnBtn_Continue()
    {
        staging.Inst.SetStateOnPlay(staging.eState.play);
    }

    [SkipRename]
    public void OnBtn_Retry()
    {
        staging.Inst.SetStateOnPlay(staging.eState.retry);
    }

    #endregion
}
