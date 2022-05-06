using UnityEngine;
using UnityEngineExJSON;

public partial class wave_abs /*load*/
{
    public int NumEdges { get { return _numEdges; } }
    public int NumDots { get { return _numDots; } }
    public float NextTime { get { return _nexttime; } }
    float _nexttime = 0.0f;
    float _endTime = 0.0f;
    int _numDots = 0;
    int _numEdges = 0;
    int _numHalf = 0;
    int _numPortal = 0;
    dot[] _dos = null;
    edge[] _edgs = null;
    halfEdge[] _halfs = null;
    portal[] _portales = null;

    public wave_abs(string strJson)
    {
        JSONObject jsWave = new JSONObject(strJson)[_jsKeyWAVE];
        _jsInfo = jsWave[_jsKeyINFO];
        _jsEdges = jsWave[_jsKeyEDGES];
        _jsDots = jsWave[_jsKeyDOTS];
        _jsHalfs = jsWave[_jsKeyHALFS];
        _jsPortals = jsWave[_jsKeyPORTALS];
        _jsGraphs = jsWave[_jsKeyGRAPHS];

        // info
        _nexttime = (float)_jsInfo["nextime"].n;

#if UNITY_EDITOR
        Debug.Log(string.Format("[editor]{0}", strJson));
#else
#endif
        _numDots = _jsDots.Count;
        _numEdges = _jsEdges.Count;
        _numHalf = _jsHalfs.Count;
        _numPortal = _jsPortals.Count;
        _dos = _numDots > 0 ? new dot[_numDots] : null;
        _edgs = _numEdges > 0 ? new edge[_numEdges] : null;
        _halfs = _numHalf > 0 ? new halfEdge[_numHalf] : null;
        _portales = _numPortal > 0 ? new portal[_numPortal] : null;
    }

    public bool IsTimeToNext()
    {
        return Time.time > _endTime;
    }
    
    public void Load(byte lvGap_ = 0)
    {
        _endTime = Time.time + _nexttime;

        // dots
        for (int i = 0; i < _numDots; i++)
        {
            jsArr arrJs = new jsArr(_jsDots[i]);
            int iType = arrJs.Int;
            _dos[i] = dots.Inst.Spawn( 
                arrJs.Byte,
                arrJs.Vec3_Of2,
                arrJs.Float );
        }

        // edges
        for (int i = 0; i < _numEdges; i++)
        {
            jsArr arrJs = new jsArr(_jsEdges[i]);
            int iType = arrJs.Int;
            byte lv = (byte)(Mathf.Clamp(arrJs.Byte + lvGap_, 1, 7));
            _edgs[i] = edges.Inst.Spawn(
                lv,
                arrJs.Vec3_Of2,
                arrJs.Vec3_Of2,
                arrJs.Float );
        }

        // HalfEdge
        for (int i = 0; i < _numHalf; i++)
        {
            jsArr arrJs = new jsArr(_jsHalfs[i]);
            halfEdge haf = _dos[arrJs.Long].AddHalf(false, arrJs.Vec3_Of2 );
            haf.type = arrJs.HalfType;
            haf.adxPair = arrJs.Short;
            for (int j = arrJs.i; j < arrJs.num; ++j)
            {
                edge edg = edges.Inst[_edgs[arrJs.Long].cdx];
                edg.SetHalf(haf);
                haf.cdxs.Push(edg.cdx);
            }

            _halfs[i] = haf;
        }

        for (int i = 0; i < _numHalf; i++)
        {
            halfEdge haf = _halfs[i];
            if (haf.adxPair != outOfAdx)
            {
                haf.pair = _halfs[haf.adxPair];
                _halfs[haf.adxPair].pair = haf;
            }
        }

        // portals
        for (int i = 0; i < _numPortal; i++)
        {
            jsArr arrJs = new jsArr(_jsPortals[i]);
            _portales[i] = portals.Inst.Open(
                arrJs.PortalType,
                arrJs.Vec3_Of2,
                arrJs.Short
                );
        }

        for (int i = 0; i < _numPortal; i++)
            _portales[i].Target = _portales[_portales[i].targetAdx];

        // Graph
        int numGraph = _jsGraphs.Count;
        for (int i = 0; i < numGraph; i++)
        {
            jsArr arrJs = new jsArr(_jsGraphs[i]);

            rDot.eState state = arrJs.rDotType;
            float spedCenter = arrJs.Float;
            float spedCross = arrJs.Float;
            float waitTime = arrJs.Float;
            dot d = _dos[arrJs.Long];
            
            rDot rd = staging.Inst.rDots.Add(
                d, state, 
                spedCenter, spedCross, waitTime);

            rd.CountDot(d);
            for (int a = arrJs.i; a < arrJs.num; ++a)
                rd.CountDot(_dos[arrJs.Long]);
            rd.Reset();
        }
    }
}
