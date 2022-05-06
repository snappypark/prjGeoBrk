using System.Collections.Generic;
using UnityEngine;

public partial class dot /*.related */
{
    enum eState
    {
        none,
        waiting,
        spwanning,
        attacking,
    }

    eState _state = eState.waiting;

    [SerializeField] public List<halfEdge> halfs = new List<halfEdge>();
    [HideInInspector] public rDot rdot;
    [HideInInspector] public Vector3 rVector;

    [HideInInspector] public int rPreToken;
    [HideInInspector] public int rCurToken;

    public halfEdge AddHalf(bool mainPair, Vector3 norDir)
    {
        halfEdge half = new halfEdge(this);
        half.norDir = norDir;
        halfs.Add(half);
        return half;
    }
}
