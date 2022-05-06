using nj;

public class rDots : Qbjs<rDot>
{
    public rDot Add(dot d, rDot.eState state_, 
        float spedCenter, float spedCross, float waitTime)
    {
        rDot rd = new rDot(d, state_, spedCenter, spedCross, waitTime);
        _q.Enqueue(rd);
        return rd;
    }

    public void Clear()
    {
        _q.Clear();
    }

    public void Update()
    {
        int numGrphs = _q.Count;
        for (int i = 0; i < numGrphs; ++i)
        {
            rDot rd = _q.Dequeue();
            if (rd.numDots == 0)
                continue;
            rd.Update();
            _q.Enqueue(rd);
        }
    }

    public static void RefreshNew(dot d)
    {
        d.rdot = staging.Inst.rDots.Add(d, d.rdot.state, 
            d.rdot.spdCenter, d.rdot.spdCross, d.rdot.waitTime);
        Refresh(d);
    }

    public static void Refresh(dot d)
    {
        d.rdot.PreReset(d);
        d.rdot.IncreaseToken(d);
        _refreshLinked(d, d.rdot);
        d.rdot.Reset();
    }

    static void _refreshLinked(dot d1, rDot rd)
    {
        rd.CountDot(d1);
        int num = d1.halfs.Count;
        for (int i = 0; i < num; ++i)
        {
            halfEdge half = d1.halfs[i];
            if (half.pair == null)
                continue;
            if ( rd.IsEqualToken( half.pair.origin ) )
                continue;

            _refreshLinked(half.pair.origin, rd);
        }
    }
}
