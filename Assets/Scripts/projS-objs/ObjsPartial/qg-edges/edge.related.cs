using UnityEngine;
using UnityEngineEx;

public partial class edge /* related */
{
    enum eState
    {
        chained,
        waiting,
        spawnning,
        attacking,
    }

    halfEdge _half = null;
    public Vector3 dirN;
    float _speed = 5.0f;

    eState _state = eState.waiting;

    [HideInInspector] public Vector3 localOfDot;
    [HideInInspector] public float length;
    [HideInInspector] public Vector3 vec1;
    [HideInInspector] public Vector3 vec2;

    Vector3 _dir2;

    public void SetHalfNull(Vector3 pos)
    {
        Vector3 v = transform.localPosition - pos;
        float dist = v.magnitude;
        _speed = Mathf.Clamp(2.1f+dist, 3, 16);

        _half = null;
        _state = eState.attacking;
        dirN = (transform.localPosition - pos).normalized;
        //dirN = (hero.Inst.transform.localPosition - transform.localPosition).normalized;
    }
    
    public void SetHalf(halfEdge haf)
    {
        _half = haf;
        localOfDot = transform.localPosition - haf.origin.transform.localPosition;
    }
}
