using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections.GenericEx;

public class ui_editwave : MonoBehaviour
{
    [SerializeField] popupWaveIO _popupParaIO;
    [SerializeField] Dropdown _dropdownParaEdti;
    [SerializeField] Dropdown _dropdownLvEdge;
    [SerializeField] Dropdown _dropdownLvDot;
    [SerializeField] Toggle _startDot;
    [SerializeField] Toggle _endDot;
    [SerializeField] Toggle _straghtDot;
    
    void OnEnable()
    {
        _dropdownParaEdti.ClearOptions();
        _dropdownParaEdti.AddOptions(new List<string>(Enum.GetNames(typeof(waveEditor.eState))));
        _dropdownLvEdge.ClearOptions();
        _dropdownLvEdge.AddOptions(ListExtensions.CreateStringIndxList(1,10));
        _dropdownLvDot.ClearOptions();
        _dropdownLvDot.AddOptions(ListExtensions.CreateStringIndxList(1, 10));
        staging.Inst.edit.SetState(waveEditor.eState.None);
        _straghtDot.isOn = true;
    }


    #region UI_ACTION Top
    public void OnDropdown_ParaEditState()
    {
        waveEditor.eState state = (waveEditor.eState)_dropdownParaEdti.value;
        staging.Inst.edit.SetState(state);
    }
    public void OnDropdown_LvEdge()
    {
        staging.Inst.edit.whatLvEdge = (byte)(_dropdownLvEdge.value + 1);
    }
    public void OnDropdown_LvDot()
    {
        staging.Inst.edit.whatLvDot = (byte)(_dropdownLvDot.value + 1);
    }

    public void OnToggle_startdot(bool value)
    {
        staging.Inst.edit.hasStartDot = _startDot.isOn;
    }
    public void OnToggle_enddot(bool value)
    {
        staging.Inst.edit.hasEndDot = _endDot.isOn;
    }
    public void OnToggle_straight(bool value)
    {
        staging.Inst.edit.isStraght = _straghtDot.isOn;
    }
    #endregion

    #region UI_ACTION Botton etc - save, load, clear, home

    public void OnBtn_PopupMenu()
    {
        _popupParaIO.gameObject.SetActive(true);
    }

    public void OnBtn_LvUp()
    {
        hero.Inst.LvUp();
    }

    public void OnBtn_InitValue()
    {
        staging.Inst.edit.SetInitValue();
    }

    #endregion

}
