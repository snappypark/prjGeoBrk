using UnityEngine;

public struct muzzle
{
    int _minLv;
    int _numLv;
    int _gapLv;
    int _fireLv;

    delay _delayFire;
    float _crSign;
    float _crLength;
    float _croRandLength;
    float _speed;
    int _hitcount;

    public muzzle(int lv_, int numLv_, float delay_, float speed_,
        float crSign_, float crLength_, int hitcount = 3, float crRandLength = 0.01f)
    {
        _minLv = lv_;
        _numLv = numLv_;
        _gapLv = 0;
        _fireLv = lv_;

        _speed = speed_;
        _crSign = crSign_;
        _crLength = crLength_;
        _croRandLength = crRandLength;
        _delayFire = new delay(delay_);
        _delayFire.Reset();
        _hitcount = hitcount;
    }

    public void ResetTime()
    {
        _delayFire.Reset();
    }

    public void Fire(Vector3 firePos, Vector3 nDir_, Vector3 cross)
    {
        if (_delayFire.IsEnd())
        {
            ballsH.Inst.Fire( firePos /* */, 
                nDir_ +
                cross *
                _crSign *
                (_crLength +
                  rand.gauss(1) *
                  _croRandLength), 
                _fireLv, _speed, _hitcount);

            _gapLv = (_gapLv + 1) % _numLv;
            _fireLv = _minLv + _gapLv;

            _delayFire.Reset();
        }
    }
}
