using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;
using nj;

[System.Serializable]
public partial class rDot: Qbj
{
    [SerializeField] public eState state;
    [SerializeField] public float spdCenter;
    [SerializeField] public float spdCross;
    [SerializeField] public float waitTime;

    [HideInInspector] public byte numDots = 0;

    [HideInInspector] public List<short> adxes = new List<short>();

    eState _preState;
    int _token = 0;
    dot _root;
    [HideInInspector] public Vector3 rootPos;
    Vector3 _posGap;
    Vector3 _center;

    Vector3 _dir;

    Rect _rect, _frame;
    float _w, _h, _halfW, _halfH;
    
    public rDot(dot root, eState state_,
        float spdCenter_, float spdCross_, float waitTime_)
    {
        state = state_;
        spdCenter = spdCenter_;
        spdCross = spdCross_;
        waitTime = waitTime_;
        PreReset(root);
    }

    public void PreReset(dot root)
    {
        _root = root;
        rootPos = _root.transform.localPosition;
        _rect = Rect.MinMaxRect(100000, 100000, -100000, -100000);
        numDots = 0;
    }

    static short[] _dots = new short[dots.capacity];
    public void CountDot(dot d)
    {
        d.rdot = this;
        d.rPreToken = d.rCurToken;
        d.rCurToken = _token;
        d.rVector = d.transform.localPosition - rootPos;
        _rect = _rect.MinMax(d.transform.localPosition);
        _dots[numDots] = d.cdx;
        ++numDots;
    }

    public void Reset()
    {
        _w = _rect.xMax - _rect.xMin;
        _h = _rect.yMax - _rect.yMin;
        _halfW = _w * 0.5f;
        _halfH = _h * 0.5f;
        _frame = stageFrame.GetByHalfSize(_halfW, _halfH);
        _center = new Vector3(_rect.xMin + _halfW, _rect.yMin + _halfH);
        _posGap = rootPos - _center;

        dot randDot = dots.Inst[_dots[Random.Range(0, numDots)]];

#if UNITY_EDITOR
        if (App.Inst.FlowMgr.HasCurFlow)
        {
            if (App.Inst.FlowMgr.CurType == ProjS.Flow.iTypeEditWave)
            {
            }
            else
            {
                if (waitTime > 0)
                {
                    _preState = state;
                    state = eState.wait;
                }
            }
        }
#else
        if (waitTime > 0)
        {
            _preState = state;
            state = eState.wait;
        }
#endif

        switch (state)
        {
            case eState.center:
                Vector3 dirN = (hero.Inst.transform.localPosition - _center).normalized;
                _dir = _gapSpeedBySkip * (spdCenter * dirN + spdCross * Vector3.Cross(dirN, Vector3.forward));
                break;
            case eState.outer:
                dirN = (_center - hero.Inst.transform.localPosition).normalized;
                _dir = _gapSpeedBySkip * (spdCenter * dirN + spdCross * Vector3.Cross(dirN, Vector3.forward));
                break;
            default: // xy
                _dir = 2.5f * spdCenter * (randDot.transform.localPosition - _center).normalized;
                break;
        }
    }

    public void IncreaseToken(dot d)
    {
        _token = d.rCurToken + 1;
    }

    public bool IsEqualToken(dot d)
    {
        return _token == d.rCurToken;
    }

    public void SetRespwanTime()
    {
        PreReset(_root);
        _token = _root.rCurToken + 1;
        _refreshLinked(_root, 0.0f);
        Reset();
    }

    const float gapSpawnTime = 0.0012f;
    void _refreshLinked(dot d1, float spawnTime)
    {
        CountDot(d1);
        d1.waiting = new delay(spawnTime);
        spawnTime += gapSpawnTime;
        int num = d1.halfs.Count;
        for (int i = 0; i < num; ++i)
        {
            halfEdge half = d1.halfs[i];

            if (half.pair == null)
                continue;
            if (half.pair.origin.rCurToken == _token)
                continue;

            _refreshLinked(half.pair.origin, spawnTime);

            Stack<short> tmp = new Stack<short>();
            while (half.cdxs.Count > 0)
                tmp.Push(half.cdxs.Pop());
            while (tmp.Count > 0)
            {
                edge edg = edges.Inst[tmp.Pop()];
                edg.waiting = new delay(spawnTime);
                spawnTime += gapSpawnTime;
                half.cdxs.Push(edg.cdx);
            }
        }
    }


}
