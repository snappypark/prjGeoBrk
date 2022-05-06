using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngine.UI;
using Mosframe;
using nj;

public class popupWaveIO : MonoBehaviour
{
    [SerializeField] Text _title;
    [SerializeField] Dropdown _dropdownWaveNextTime;

    static int _waveIdx = 0;
    int time = 1;
    void OnEnable()
    {
        _title.text = string.Format("wave {0}", _waveIdx);
        _dropdownWaveNextTime.ClearOptions();
        _dropdownWaveNextTime.AddOptions(ListExtensions.CreateStringIndxList(1,20));
        _dropdownWaveNextTime.value = time-1;
    }

    #region UI Action

    public void OnBtn_IdxUp()
    {
        _waveIdx = Mathf.Clamp(_waveIdx + 1, 0, 99);
        _title.text = string.Format("wave {0}", _waveIdx);
    }

    public void OnBtn_IdxDown()
    {
        _waveIdx = Mathf.Clamp(_waveIdx - 1, 0, 99);
        _title.text = string.Format("wave {0}", _waveIdx);
    }

    public void OnBtn_ResetIdx()
    {
        _waveIdx = 0;
        _title.text = string.Format("wave {0}", _waveIdx);
    }

    public void OnBtn_Duplicate()
    {
        int nexttime = 1 + _dropdownWaveNextTime.value * 1;// magic number

        staging.Inst.edit.SaveToLocal(_waveIdx, nexttime);
        ///*
       // staging.Inst.ClearObjs();
      //  staging.Inst.edit.Clear();
        //*/
        gameObject.SetActive(false);
    }

    public void OnBtn_Save()
    {
        int nexttime = 1 + _dropdownWaveNextTime.value * 1;// magic number

        staging.Inst.edit.SaveToLocal(_waveIdx++, nexttime);
        ///*
        staging.Inst.ClearObjs();
        staging.Inst.edit.Clear();
        //*/
        gameObject.SetActive(false);
    }

    public void OnBtn_Load()
    {
        time = (int)(staging.Inst.edit.LoadFromLocal(_waveIdx) + 0.2f);
        gameObject.SetActive(false);
    }


    public void OnBtn_Close()
    {
        gameObject.SetActive(false);
    }

    public void OnBtn_Clear()
    {
        staging.Inst.ClearObjs();
        staging.Inst.edit.Clear();
        gameObject.SetActive(false);
    }

    public void OnBtn_Home()
    {
        staging.Inst.ClearObjs();
        staging.Inst.edit.Clear();
        staging.Inst.edit.SetState(waveEditor.eState.None);
        App.Inst.FlowMgr.Change<ProjS.Flow_Menu>();
        gameObject.SetActive(false);
    }
    #endregion
}
