using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;

[System.Serializable]
public class halfEdge
{
    public enum eType
    {
        none = 0,
        wipeWipe, // 갈대
        razorShell, // 맛조개
                    // wave,
                    //leech, // 거머리
    }

    public eType type;
    public Vector3 norDir;

    [HideInInspector] public dot origin = null;
    [HideInInspector] public short adx = 0;
    [HideInInspector] public halfEdge pair = null;
    [HideInInspector] public short adxPair = 0;
    
    [HideInInspector] public Stack<short> cdxs = new Stack<short>();

    public Rect rect = new Rect();

    public halfEdge(dot originDot)
    {
        origin = originDot;
    }

    public void Clear(Vector3 pos)
    {
        if (pair != null)
            pair.ClearCdx(pos);
        ClearCdx(pos);
    }

    public void ClearCdx(Vector3 pos)
    {
        while (cdxs.Count > 0)
            edges.Inst[cdxs.Pop()].SetHalfNull(pos);
        pair = null;
    }
}
