using UnityEngine;
using UnityEngineEx;

public class FpDrawDot : MonoBehaviour
{
    public FpLineDraw Dot = null;
    public FpLineDraw Bone = null;

    public void Refresh(Vector3 dotPt)
    {
        Dot.Active(dotPt);
        Bone.Active(dotPt);
    }
}
