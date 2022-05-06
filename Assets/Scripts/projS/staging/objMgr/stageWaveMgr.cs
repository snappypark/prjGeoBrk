using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

//[System.Serializable]
public class stageWaveMgr
{
    List<wave_abs> _waves = new List<wave_abs>();

    public scInt totalGoal = new scInt();

    int _curIdx;
    int _nextIdx;
    int _numWave;

    byte _lvGap;

    public bool IsLastWave { get { return _nextIdx == _numWave; } }
    
    public IEnumerator OnStartStage_()
    {
        _curIdx = 0;
        _nextIdx = 0;
        _numWave = stageDesign.GetNumWave();
        _lvGap = (byte)(User.Inst.CurStageIdx / stageDesign.LoopCnt) ;

        _waves.Clear();
        for (int i = 0; i < _numWave; ++i)
        {   
            _waves.Add(new wave_abs( FileIO.Res.PasingWave(i) ));
            //yield return new WaitForSeconds(0.05f);
            //yield return null;
        }
        
        totalGoal.SetValue(0);
        for (int i = 0; i < _numWave; ++i)
            totalGoal.SetValue(totalGoal.GetValue() + _waves[i].NumEdges);

        yield return null;
    }

    public void OnEndStage()
    {
        _waves.Clear();
    }

    public void LoadWave_QueStr()
    {
        if (_nextIdx < _numWave)
        {
            if (edges.Inst.IsOverFlower(_waves[_nextIdx].NumEdges))
                return;
            if (dots.Inst.IsOverFlower(_waves[_nextIdx].NumDots))
                return;
            _waves[_nextIdx].Load(_lvGap);
            _curIdx = _nextIdx;
            _nextIdx = 1 + _curIdx;
            UIs.Inst.Playing.RefreshEdgeNum();
        }
    }

    public void Update()
    {
        if (_waves[_curIdx].IsTimeToNext() || edges.Inst.IsEmpty())
            LoadWave_QueStr();
    }
}
