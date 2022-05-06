using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class dots : ObjsPartialPool<dots, dot>
{
    [HideInInspector] public const short capacity = 196;
    public const byte type0 = 0;

    protected override short CapacityOfType(byte type) { return capacity; }
    
    public dot Spawn(  byte lv, Vector3 pos, float spawnTime = 0.0f)
    {
        dot d = Reuse(type0, pos);
        if (d != null)
        {
            d.AssignByLv(lv);
            d.waiting = new delay(spawnTime);
            d.waiting.Reset();
            d.spawnning = new delay(0.12f);
            //d.draw.SetStartAndEndPoints(new Vector3(0, 0), new Vector3(0, 0));
            d.draw.SetStartAndEndPoints(new Vector3(-0.0001f, 0, 2), new Vector3(0.0001f, 0, 2));
                    
        //    d.tnf = new toNfro(0.007f, 0.007f, rand.gauss(1)* 0.07f);
        }
        return d;
    }

    public void Move(dot obj)
    {
        _Move(obj);
    }

    public halfEdge[] FindAllHalfedge_ByArrDots(dot[] arrDots)
    {
        if (arrDots == null || arrDots.Length == 0)
            return null;
        List<halfEdge> lst = new List<halfEdge>();
        short adx = 0;
        for (int i = 0; i < arrDots.Length; ++i)
        {
            dot d = arrDots[i];
            int numHalf = d.halfs.Count;
            for (int j = 0; j < numHalf; ++j)
            {
                halfEdge half = d.halfs[j];
                half.adx = adx++;
                lst.Add(half);
            }
        }
        return lst.Count == 0 ? null : lst.ToArray();
    }

    public bool IsOverFlower(int num)
    {
        return _pool[type0].Remain < num;
    }
}
