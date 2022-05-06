using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class portals : ObjsShortQuePool<portals, portal>
{
    public enum eType
    {
        tunnel,
    }

    short[] _capacitys = new short[] { 10};
    protected override short CapacityOfType(byte type) { return _capacitys[type]; }

    public portal Open(eType type, Vector3 pos, short adx)
    {
        portal p = _pool[(int)type].Reuse(pos);
        if (null != p)
        {
            p.targetAdx = adx;
        }
        return p;
    }

    public void Close(portal obj)
    {
        _pool[obj.type].Unuse(obj);
    }
}

