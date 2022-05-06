using UnityEngine;
using UnityEngineEx;
using VolumetricLines;
using nj;

public partial class edge : pqObj
{
    [SerializeField] public edgeStats stats = new edgeStats();
    [SerializeField] public VolumetricLineBehavior draw;
    [SerializeField] public CircleCollider2D collision;

    public Vector3 Nor;
    public Vector3 Pos1 { get { return transform.localPosition + new Vector3(draw.StartPos.x, draw.StartPos.y); } }
    public Vector3 Pos2 { get { return transform.localPosition + new Vector3(draw.EndPos.x, draw.EndPos.y); } }

    public override void OnClone()
    {
        _state = eState.waiting;
    }
    
    public override void OnUnuse(eUnuse type_ = eUnuse.Default)
    {
        switch (type_)
        {
            case eUnuse.Default:
                effs.Inst.ExplosionEdge(transform.localPosition, dirN);

                AppAudio.Inst.PlaySound(AppAudio.eSoundType.hit);
                staging.Inst.CheckWin_CountDestroyedObj();
                UIs.Inst.Playing.RefreshEdgeNum();
                break;
            default:
                if (Test.Active)
                    staging.Inst.CheckWin_CountDestroyedObj();
                break;
        }
        
        if (_half != null)
        {
            dot d1 = _half.origin;
            dot d2 = _half.pair.origin;

            _half.Clear(transform.localPosition);

            //graph
            rDots.Refresh(d1);
            d1.rdot.SetNextTypeBySplit(transform.localPosition);
            if (d1.rPreToken == d2.rCurToken)
            {
                rDots.RefreshNew(d2);
                d2.rdot.SetNextTypeBySplit(transform.localPosition);
            }
        }
        _state = eState.waiting;
    }

    nj.skip edgeRotate = new nj.skip(4);
    void Update()
    {
#if UNITY_EDITOR
        if (App.Inst.FlowMgr.CurType == (int)ProjS.Flow.eType.EditWave)
        {
            draw.SetStartAndEndPoints(vec1, vec2);
            return;
        }
#endif

        stats.ResetHudLightSaber( ); // tnf.GetDt()

        switch (_state)
        {
            case eState.waiting:
                if (null != _half)
                    transform.localPosition = _half.origin.transform.localPosition + localOfDot;
                if (waiting.IsEnd())
                {
                    spawnning.Reset();
                    _state = eState.spawnning;
                }
                break;
            case eState.spawnning:
                if (null != _half)
                    transform.localPosition = _half.origin.transform.localPosition + localOfDot;
                if (spawnning.IsEnd())
                {
                    draw.SetStartAndEndPoints(vec1, vec2);
                    _state = (_half != null) ? eState.chained : eState.attacking;
                }
                else
                    draw.SetStartAndEndPoints(vec1, vec1 + (vec2- vec1) * spawnning.Ratio01());
                break;
            case eState.attacking:
                _speed = Mathf.Max(_speed * 0.989f, 0.9f);
                Vector3 next = transform.localPosition + _speed * dirN * Time.smoothDeltaTime;


                if (next.x < stageFrame.left) dirN.x = -dirN.x;
                if (next.x > stageFrame.right) dirN.x = -dirN.x;
                if (next.y < stageFrame.bottom) dirN.y = -dirN.y;
                if (next.y > stageFrame.top) dirN.y = -dirN.y;


                transform.localPosition = new Vector3(
                    Mathf.Clamp(next.x, stageFrame.left, stageFrame.right),
                    Mathf.Clamp(next.y, stageFrame.bottom, stageFrame.top)
                    );

                edgeRotate.OnUpdate(delegate() {
                    
              //      dirN = _dir2.FastNorOnXY();


                    //Vector3 halfDirN = Vector3.ClampMagnitude(dirN * 0.39f, 0.9f);
                    //draw.SetStartAndEndPoints(-halfDirN, halfDirN);
                    ///*
                    Vector3 dir = hero.Inst.transform.localPosition - transform.localPosition;

                    float dot = Vector3.Dot(dirN, dir);
                    if (dot * dot < dir.sqrMagnitude - 0.02f)
                    {
                        dirN = ((dirN + dir.FastNorOnXY() * 0.147f)).normalized;

                        Vector3 halfDirN = Vector3.ClampMagnitude(dirN * 0.39f, 0.9f);
                        draw.SetStartAndEndPoints(-halfDirN, halfDirN);

                    }
                    else
                    {
                        Vector3 halfDirN = Vector3.ClampMagnitude(dirN * 0.40f, 0.9f);
                        draw.SetStartAndEndPoints(-halfDirN, halfDirN);
                    }//*/
                });

                break;
            case eState.chained:
                if(null != _half)
                    transform.localPosition = _half.origin.transform.localPosition + localOfDot;
                break;
        }
        edges.Inst.Move(this);
    }
}
