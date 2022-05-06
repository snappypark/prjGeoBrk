using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public partial class waveEditor
{
    public float initSpedCenter = 0.5f;
    public float initSpedCross = 0.4f;
    public float initWaitTime = 0.7f;
    public rDot.eState initDotState = rDot.eState.center;

    [SerializeField] public List<rDot> rdots = new List<rDot>();
    wave_abs _wave = null;

    public byte whatLvEdge { get; set; }
    public byte whatLvDot { get; set; }
    public bool hasStartDot { get; set; }
    public bool hasEndDot { get; set; }
    public bool isStraght { get; set; }

    public void Clear()
    {
        rdots.Clear();
    }

    public void SetInitValue()
    {

        int numGrphs = rdots.Count;
        for (int i = 0; i < numGrphs; ++i)
        {
            rdots[i].spdCenter = initSpedCenter;
            rdots[i].spdCross = initSpedCross;
            rdots[i].waitTime = initWaitTime;
            rdots[i].state = initDotState;
        }
    }

    public float LoadFromLocal(int count)
    {
        Debug.Log(count);
        rdots.Clear();
        staging.Inst.rDots.Clear();
        _wave = new wave_abs(FileIO.Local.Read(string.Format("{0}", count), "txt"));
        _wave.Load();

        int numGrphs = staging.Inst.rDots.Count;
        for (int i = 0; i < numGrphs; ++i)
            rdots.Add(staging.Inst.rDots.Dequeue);
        
        if (rdots.Count > 0)
        {
            initSpedCenter = rdots[0].spdCenter;
            initSpedCross = rdots[0].spdCross;
            initWaitTime = rdots[0].waitTime;
            initDotState = rdots[0].state;
        }

        return _wave.NextTime;
    }

    public void SaveToLocal(int count, int nexttime)
    {
        FileIO.Local.Write(string.Format("{0}", count), wave_abs.BakeJSON(nexttime), "txt");
    }

    void _generateGraphs()
    {
        rdots.Clear();
        staging.Inst.rDots.Clear();
        int numObj = dots.Inst.NumObj;
        if (numObj == 0)
            return;

        dot[] arrDots = dots.Inst.FindObjs_InPartials();
        rDot rd = staging.Inst.rDots.Add(arrDots[0], initDotState,
            initSpedCenter, initSpedCross, initWaitTime);

        for (int i = 0; i < numObj; ++i)
            rd.CountDot(arrDots[i]);
        
        rDots.Refresh(arrDots[0]);

        for (int i = 1; i < numObj; ++i)
            if (arrDots[0].rPreToken == arrDots[i].rCurToken)
                rDots.RefreshNew(arrDots[i]);

        int numGrphs = staging.Inst.rDots.Count;
        for (int i = 0; i < numGrphs; ++i)
            rdots.Add(staging.Inst.rDots.Dequeue);
    }
}
