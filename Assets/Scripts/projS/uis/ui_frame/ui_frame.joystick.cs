using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngineEx;

using Beebyte.Obfuscator;

using nj;

public partial class ui_frame
{
    [System.Serializable]
    class joystick
    {
        [SerializeField] public ScrollRect Scroll = null;
        [SerializeField] public RectTransform TransBtn = null;
        [HideInInspector] public float sqrDist = 0.0f;
        [HideInInspector] public Vector2 Dir = Vector2.zero;
    }

    [SerializeField] joystick _joystick;
    float _sqrRange = 325.0f;

    public bool IsMoveJoystic = false;
    void Update()
    {
#if UNITY_EDITOR
        if (App.Inst.FlowMgr.HasCurFlow)
        {
            if (App.Inst.FlowMgr.CurType == ProjS.Flow.iTypeEditWave)
                _joystick.Scroll.transform.localPosition = new Vector3(0, -150);
            else
                _joystick.Scroll.transform.localPosition = new Vector3(0, -340);
        }
#endif
        /*
        short numobj = edges.Inst.NumObj;
        _textEdgeCnt.text = string.Format("edges:{0}", numobj);
        numobj = dots.Inst.NumObj;
        _textDotCnt.text = string.Format("dots:{0}", numobj);
        */

        IsMoveJoystic = _joystick.TransBtn.anchoredPosition.sqrMagnitude > _sqrRange;
        switch (IsMoveJoystic)
        {
            case true:
                hero.Inst.RotateAndFire(
                    new Vector3(-_joystick.TransBtn.anchoredPosition.x, -_joystick.TransBtn.anchoredPosition.y, 0).normalized);
                break;
            case false:
                hero.Inst.ResetFire();
                break;
        }
        #region lagacy move joystick
        /*
        //  bool bRotationed = false;
        if (_joystickMove.TransBtn.anchoredPosition.magnitude > _JoystickRange.x)
        {
            _joystickMove.Dist = _joystickMove.TransBtn.anchoredPosition.magnitude;
            _joystickMove.Dir = _joystickMove.TransBtn.anchoredPosition.normalized;
            Vector3 dir = new Vector3(_joystickMove.TransBtn.anchoredPosition.x, _joystickMove.TransBtn.anchoredPosition.y, 0).normalized;

            hero.Inst.Move(dir, bRotationed);
        }
        */
        #endregion
    }

    #region UI Action
    [SkipRename]
    public void OnScroll_JoystickMove(Vector2 value)
    {
        /*
        if (_joystick.sqrDist < _sqrRange)
            return;
        switch (App.Inst.FlowMgr.CurType)
        {
            case ProjS.Flow.iTypeMenu:
                break;
            case ProjS.Flow.iTypePlay:
              //  _joystickAct.TransBtn.anchoredPosition = _joystickAct.Dir * _JoystickRange.y;
                break;
        }*/
    }

    #endregion
}
