using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct delay
{
    bool _onceAfterTime;
    public float duration;
    float _durationOver;
    float _nextTime;
    public delay(float duration_, bool onceAfterTime_ = true)
    {
        _onceAfterTime = onceAfterTime_;
        duration = duration_;
        _durationOver = 1 / duration;
        _nextTime = 0;
    }

    public void Reset()
    {
        _onceAfterTime = true;
        _nextTime = Time.time + duration;
    }

    public void Reset(float waittime)
    {
        duration = waittime;
        _durationOver = 1 / duration;
        _onceAfterTime = true;
        _nextTime = Time.time + duration;
    }

    public bool IsEnd()
    {
        return Time.time > _nextTime;
    }

    public bool InTime()
    {
        return Time.time < _nextTime;
    }

    public float Ratio10()
    {
        return Mathf.Clamp01((_nextTime - Time.time)* _durationOver);
    }

    public float Ratio01()
    {
        return 1-Mathf.Clamp01((_nextTime - Time.time) * _durationOver);
    }

    public bool afterOnceTime()
    {
        if (_onceAfterTime && Time.time > _nextTime)
        {
            _onceAfterTime = false;
            return true;
        }
        return false; 
    }
}
