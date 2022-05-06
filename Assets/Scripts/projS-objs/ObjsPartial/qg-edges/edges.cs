using UnityEngine;
using nj;

public class edges : ObjsPartialPool<edges, edge>
{
    [HideInInspector] public const short capacity = 196;
    public const byte type0 = 0;

    protected override short CapacityOfType(byte type) { return capacity; }
    
    public edge Spawn(byte lv, Vector3 pos1, Vector3 pos2, float spawnTime = 0.0f)
    {
        Vector3 pos = Vector3.Lerp(pos1, pos2, 0.5f) ;
        edge e = Reuse(type0, pos);
        if (e != null)
        {
            //e.draw.SetStartAndEndPoints(pos1 - pos, pos2 - pos);
            e.draw.SetStartAndEndPoints(pos1 - pos, pos1 - pos);
            e.vec1 = pos1 - pos;
            e.vec2 = pos2 - pos;
            e.waiting = new delay(spawnTime);
            e.waiting.Reset();
            e.spawnning = new delay(0.012f);
            e.Nor = (pos2 - pos1).normalized;

            Vector3 v = (pos1 - pos2);
            e.length = v.magnitude;
            e.stats.Init(lv);
            e.dirN = v.normalized;
        }
        return e;
    }

    public void Move(edge obj)
    {
        _Move(obj);
    }

    public bool IsOverFlower(int num)
    {
        return _pool[type0].Remain < num;
    }


    public bool IsEmpty()
    {
        return _pool[type0].Remain == capacity;
    }
}
