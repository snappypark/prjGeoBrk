using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class heroes : Objs<heroes, Obj>
{
    public enum eType
    {
        A,
        B,
    }

    public Obj Create(eType type, Vector3 pos, Transform parent)
    {
        Obj go = CloneObj((byte)type, parent);
        go.transform.localPosition = pos;
        return go;
    }

}
