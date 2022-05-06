using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class subs : Objs<subs, Obj>
{
    public enum eType
    {
        a=0,
        b,
        Max,
    }
    
    public Obj Create(eType type, Vector3 pos, Transform parent)
    {
        Obj go = CloneObj((byte)type, parent);
        go.transform.localPosition = pos;
        return go;
    }

}
