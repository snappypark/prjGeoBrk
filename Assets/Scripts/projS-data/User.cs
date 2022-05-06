using UnityEngine;

using nj;

// If you're going to use any obscured types / prefs from code you'll need to use this name space:
using CodeStage.AntiCheat.ObscuredTypes;

public partial class User : MonoSingleton<User>
{
    [Header("Other")]
    public string gi = "lineshot-stage-1.1.0";
    
    private void OnValidate()
    {
        if (Application.isPlaying) ObscuredPrefs.CryptoKey = gi;
    }

    private void OnApplicationQuit()
    {
        //DeleteRegularPrefs();
    }

    public StageAds StageAd = new StageAds();

    public scInt PreStageIdx = new scInt();
    public scInt CurStageIdx = new scInt();
    public scInt CurLv = new scInt();
    public scInt CurExp = new scInt();

    public scBool Defeated = new scBool();

    public void Load()
    {
#if UNITY_EDITOR
        edit_reset_z();
#else
#endif
        StageAd.Load();
        CurStageIdx.SetValue(ObscuredPrefs.GetInt("xdis", 0));
        CurLv.SetValue(ObscuredPrefs.GetInt("vl", 1));
        CurExp.SetValue(ObscuredPrefs.GetInt("pxe", 0));

        Defeated.SetValue(false);

#if UNITY_EDITOR
        ///*
        CurStageIdx = 0;//216;
        CurLv = 1;//(int)(CurStageIdx*0.43f);
        CurExp = 0;
        // */
#else
#endif
        PreStageIdx.SetValue(CurStageIdx.GetValue()) ;
    }
    
    public void SaveLv()
    {
        ObscuredPrefs.SetInt("xdis", CurStageIdx);
        ObscuredPrefs.SetInt("vl", CurLv);
        ObscuredPrefs.SetInt("pxe", CurExp);

        ObscuredPrefs.Save();
    }
    
    public void edit_reset_z()
    {
        StageAd.edit_reset_z();

        ObscuredPrefs.SetInt("xdis", 0);
        ObscuredPrefs.SetInt("vl", 1);
        ObscuredPrefs.SetInt("pxe", 0);
        
        ObscuredPrefs.Save();
    }
}
