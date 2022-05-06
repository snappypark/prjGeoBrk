using UnityEngine;
using UnityEngineEx;
using VolumetricLines;

using nj;

public class ballH : qObj
{
    [SerializeField] public VolumetricLineBehavior draw;
    [SerializeField] public CircleCollider2D collision;
    public Vector3 dir;
    public float speed;
    public int dmg;
    public int lv;

    public override void OnUnuse(eUnuse type_ = eUnuse.Default)
    {
    }
    
    public int count = 17;
    delay _collisionTime = new delay(0.29f);
    //OnPortal _onPortal = new OnPortal();

    nj.skip collisionEdge = new nj.skip(4);
    void Update()
    {
        Vector3 dirSpeed = dir * speed;
        transform.localPosition += dirSpeed * Time.smoothDeltaTime;

        if (stageFrame.IsOutFrmae(transform.localPosition))
        {
            ballsH.Inst.Unuse(this);
            return;
        }

        Pt pt = transform.Pt();
        collisionEdge.OnUpdate(()=> {
            edges.Inst.Query_EnqueOrDequeByReturn(pt, delegate (edge obj)
            {
                //if (_collisionTime.InTime())
                //    return ObjsPartialPool<edges, edge>.qReturn.Next;
                Vector3 nearestPos = transform.localPosition.NearestPos_OnLine(obj.Pos1, obj.Pos2);
                float dist = collision.radius + obj.collision.radius;

                if (transform.localPosition.IsFarFrom(nearestPos, dist))
                    return ObjsPartialPool<edges, edge>.qReturn.Next;

                /*
                Vector3 prePos = transform.localPosition - 5 * dir;
                float d = Vector3.Dot(obj.Nor, (nearestPos - prePos));
                dir = ((nearestPos - d*obj.Nor) - prePos).normalized;
                transform.SetAngleOnXY(dir);
                */
                effs.Inst.ExplosionBall(nearestPos, draw.LineColor, -dir);

//                obj.Nor
                obj.stats.GotDamaged(dmg);

                //_collisionTime.Reset();
               // count--;
               // if (count == 0)
                ballsH.Inst.Unuse(this);
                if (obj.stats.isAlive)
                    return ObjsPartialPool<edges, edge>.qReturn.Next_Exit;
                return ObjsPartialPool<edges, edge>.qReturn.Unuse_Exit;
            });
        });
    }
    
}
