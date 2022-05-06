using System.Collections;
using UnityEngine;
using UnityEngineEx;
using nj;

namespace ProjS
{
    public static class Flow_Start
    {
        public static IEnumerator OnStart()
        {
            cams.Inst.SetOrthInfo(coord.CamOrthInfo_Play);
#if UNITY_EDITOR
#else
            UIs.Inst.Cover.Img.SetAlpha(1.0f);
#endif
            yield return UIs.Inst.Cover.Active_();
        }
    }

    public class Flow_Entry : Flow
    {
        protected override eType Type { get { return eType.Entry; } }

        public override IEnumerator OnEnter_()
        {
            ads.Inst.Init();
            //yield return UIs.Inst.Entry.Active_();
            //yield return UIs.Inst.Cover.Img.FadeOut_();
            //yield return new WaitForSeconds(0.9f);
            App.Inst.FlowMgr.Change<Flow_Menu>();
            yield return null;
        }

        public override IEnumerator OnExit_()
        {
            User.Inst.Load();
            //AppAudio.Inst.PlayMusic(AppAudio.eMusicType.outgame);
            //yield return new WaitForSeconds(0.6f);
            //yield return UIs.Inst.Cover.Img.FadeIn_();
            //yield return UIs.Inst.Entry.Inactive_();
            yield return null;
        }
    }
        
    public class Flow_Menu : Flow
    {
        protected override eType Type { get { return eType.Menu; } }

        public override IEnumerator OnEnter_()
        {
            UIs.Inst.Frame.RefreshStageAndLv();
            ambiance.Inst.SetByStageIdx();
            hero.Inst.Init();
            yield return UIs.Inst.Menu.Active_();
            if (App.Inst.FlowMgr.PreType == iTypeEntry)
                yield return UIs.Inst.Cover.Img.FadeOut_();
            else
                yield return UIs.Inst.Menu.context.FadeIn_();
        }

        public override IEnumerator OnExit_()
        {
            yield return UIs.Inst.Menu.context.FadeOut_();
            yield return UIs.Inst.Menu.Inactive_();
        }
    }
    
    public class Flow_Play : Flow
    {
        protected override eType Type { get { return eType.Play; } }

        public override IEnumerator OnEnter_()
        {
            ambiance.Inst.SetChanging();
            hero.Inst.Init();
            yield return UIs.Inst.Playing.Active_();
            yield return staging.Inst.Start_();
        }

        public override IEnumerator OnExit_()
        {
            yield return staging.Inst.End_();
            yield return UIs.Inst.Playing.Inactive_();
        }
    }
    
    public class Flow_EditWave : Flow
    {
        protected override eType Type { get { return eType.EditWave; } }

        public override IEnumerator OnEnter_()
        {
            yield return UIs.Inst.EditWave.Active_();
        }

        public override IEnumerator OnExit_()
        {
            yield return UIs.Inst.EditWave.Inactive_();
        }
    }
}