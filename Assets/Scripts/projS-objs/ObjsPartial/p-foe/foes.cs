using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using nj;

public class foes : ObjsPartialPool<foes, foe>
{
    public enum eType
    {
        type0,
    }

    short[] _capacitys = new short[] { 1 };
    protected override short CapacityOfType(byte type) { return _capacitys[type]; }

    byte tmpLv = 1;
    public foe Spawn(eType type, float x, float y)
    {
        foe f = Reuse((byte)type, new Vector3(x, y));
        if (null != f)
        {
            f.AssignByLv(tmpLv);
            tmpLv++;
            if (tmpLv == 11)
                tmpLv = 1;
        }
        return f;
    }

    public void Move(foe obj)
    {
        _Move(obj);
    }

}
