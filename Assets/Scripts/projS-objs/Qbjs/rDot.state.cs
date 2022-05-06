using UnityEngine;
using UnityEngineEx;

public partial class rDot
{
    public enum eState
    {
        none = -1,
        center = 0,
        xy,
        outer,

        wait,
    }
    
    public void SetNextTypeBySplit(Vector3 from_)
    {
        switch (state)
        {
            case eState.none:
                state = eState.center;
                break;
            case eState.xy:
                state = eState.center;
                break;
            case eState.outer:
                state = eState.center;
                break;
            case eState.wait:
                waitTime = -1;
                _dir = 2.5f * spdCenter * (_center - from_).normalized;
                state = eState.xy;
                break;
            default:
                _dir = 2.5f * spdCenter * (_center - from_).normalized;
                state = eState.xy;
                break;
        }
    }

    nj.skip _changeDir = new nj.skip(21);
    const float _gapSpeedBySkip = 2.45f;
    public void Update()
    {
        switch (state)
        {
            case eState.none:
                state = eState.center;
                break;
            case eState.wait:
                waitTime -= Time.smoothDeltaTime;
                if (waitTime < 0)
                    state = _preState;
                break;
            case eState.center:
                _changeDir.OnUpdate(() => {
                    Vector3 dir = hero.Inst.transform.localPosition - _center;
                    Vector3 dirN = dir.normalized;
                    //_dir.FastNorOnXY();
                    _dir = _gapSpeedBySkip * (spdCenter* dirN  + spdCross * Vector3.Cross(dirN, Vector3.forward));

                    if (dir.sqrMagnitude < 0.7f)
                        state = eState.outer;
                });

                _center += _dir * Time.smoothDeltaTime;
                _center.x = Mathf.Clamp(_center.x, _frame.xMin, _frame.xMax);
                _center.y = Mathf.Clamp(_center.y, _frame.yMin, _frame.yMax);
                rootPos = _center + _posGap;
                break;
            case eState.xy:
                _center += _dir * Time.smoothDeltaTime;

                _changeDir.OnUpdate(() => {
                    if (_center.x < _frame.xMin && _dir.x < 0) _dir.x = -_dir.x;
                    if (_center.x > _frame.xMax && _dir.x > 0) _dir.x = -_dir.x;
                    if (_center.y < _frame.yMin && _dir.y < 0) _dir.y = -_dir.y;
                    if (_center.y > _frame.yMax && _dir.y > 0) _dir.y = -_dir.y;
                });
                
                _center.x = Mathf.Clamp(_center.x, _frame.xMin, _frame.xMax);
                _center.y = Mathf.Clamp(_center.y, _frame.yMin, _frame.yMax);
                rootPos = _center + _posGap;
                break;
            case eState.outer:
                _center += _dir * Time.smoothDeltaTime;

                _changeDir.OnUpdate(()=> {
                    Vector3 dir = _center - hero.Inst.transform.localPosition;
                    Vector3 dirN = (_center - hero.Inst.transform.localPosition).normalized;
                  //  ((_dir + dir.FastNorOnXY() * 0.147f)).FastNorXY();
                    //_dir.FastNorOnXY();
                    _dir = _gapSpeedBySkip * (spdCenter * dirN + spdCross * Vector3.Cross(dirN, Vector3.forward));
                    if (_center.x < _frame.xMin || _center.x > _frame.xMax ||
                        _center.y < _frame.yMin || _center.y > _frame.yMax)
                        state = eState.center;
                });

                _center.x = Mathf.Clamp(_center.x, _frame.xMin, _frame.xMax);
                _center.y = Mathf.Clamp(_center.y, _frame.yMin, _frame.yMax);
                rootPos = _center + _posGap;
                break;
        }
    }


}
