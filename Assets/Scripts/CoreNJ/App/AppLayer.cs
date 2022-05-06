using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nj
{ 
public static class AppLayer
{
    public static int DefaultMask = 0;
    public static int TouchPlanMask;

    public static string LAYERNAME_DEFAULT = "Default";
    public static string LAYERNAME_TOUCH_PLAN = "TouchPlan";

    public static void Awake()
    {
        DefaultMask = 1 << LayerMask.NameToLayer(LAYERNAME_DEFAULT);
        TouchPlanMask = 1 << LayerMask.NameToLayer(LAYERNAME_TOUCH_PLAN);
    }
}
}