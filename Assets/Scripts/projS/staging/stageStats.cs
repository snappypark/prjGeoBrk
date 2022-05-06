using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class stageStats
{
    public scInt gainGold = new scInt();
    public rInt goal;

    public void Reset(scInt goal_)
    {
        gainGold.SetValue(0);
        goal = new rInt(0, goal_);
        
        UIs.Inst.Frame.RefreshProgress(goal.Ratio01());
    }
}
