using UnityEngine;

using nj;

public partial class ambiance : MonoSingleton<ambiance>
{
    public enum eState
    {
        None,
        Changing,
    }

    [SerializeField] Camera _mainCam;
    [SerializeField] LightRays2D[] _bgLightRays;
    [SerializeField] SpriteRenderer[] _sps;

    eState _state = eState.None;

    nj.skip _changeColor = new nj.skip(13);
    nj.timer _changinTimer = new nj.timer(17);

    public void SetChanging()
    {
        _changinTimer.Reset();
        if(IsChanged())
            _state = eState.Changing;
    }

    public void SetByStageIdx()
    {
        int stageIdx = User.Inst.CurStageIdx;
        SetCurColors((stageIdx+1) % numColor);
        SetNextToCurColor();
        SetCurColors(stageIdx % numColor);
        _mainCam.backgroundColor = _curCol1;
        _sps[0].color = _curCol2;
        _sps[1].color = _curCol2;
        _bgLightRays[0].color1 = _curCol3;
        _bgLightRays[0].color2 = _curCol4;
        _bgLightRays[1].color1 = _curCol3;
        _bgLightRays[1].color2 = _curCol4;
        _bgLightRays[0].OnUpdate();
        _bgLightRays[1].OnUpdate();
        _state = eState.None;
    }

    void Update()
    {
        switch (_state)
        {
            case eState.Changing:
                _changeColor.OnUpdate(()=> {
                if (_changinTimer.InDuration())
                {
                    float ratio01 = _changinTimer.time.Ratio01();
                    _mainCam.backgroundColor = Color.Lerp(_curCol1, _nextCol1, ratio01);
                    _sps[0].color = Color.Lerp(_curCol2, _nextCol2, ratio01);
                    _sps[1].color = _sps[0].color;
                    _bgLightRays[0].color1 = Color.Lerp(_curCol3, _nextCol3, ratio01);
                    _bgLightRays[1].color1 = _bgLightRays[0].color1;
                    _bgLightRays[0].color2 = Color.Lerp(_curCol4, _nextCol4, ratio01);
                    _bgLightRays[1].color2 = _bgLightRays[0].color2;
                    _bgLightRays[0].OnUpdate();
                    _bgLightRays[1].OnUpdate();
                 }
                else
                    _state = eState.None;
                });
                break;
            case eState.None:

#if UNITY_EDITOR
                _bgLightRays[0].OnUpdate();
                _bgLightRays[1].OnUpdate();
#endif
                break;
        }
    }

    public string printColors()
    {
        return string.Format(
            "_curCol1 = new Color({0:0.000}f, {1:0.000}f, {2:0.000}f);\n_curCol2 = new Color({3:0.000}f, {4:0.000}f, {5:0.000}f);\n_curCol3 = new Color({6:0.000}f, {7:0.000}f, {8:0.000}f);\n_curCol4 = new Color({9:0.000}f, {10:0.000}f, {11:0.000}f);",
            _mainCam.backgroundColor.r, _mainCam.backgroundColor.g, _mainCam.backgroundColor.b,
            _sps[0].color.r, _sps[0].color.g, _sps[0].color.b,
            _bgLightRays[0].color1.r, _bgLightRays[0].color1.g, _bgLightRays[0].color1.b,
            _bgLightRays[0].color2.r, _bgLightRays[0].color2.g, _bgLightRays[0].color2.b);
    }
}
