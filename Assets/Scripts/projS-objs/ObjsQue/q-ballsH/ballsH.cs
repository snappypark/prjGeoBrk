using UnityEngine;
using UnityEngineEx;

using nj;

public class ballsH : ObjsQuePool<ballsH, ballH>
{
    short[] _capacitys = new short[] { 96 };
    protected override short CapacityOfType(byte idx) { return _capacitys[idx]; }
    
    public ballH Fire(Vector3 pos, Vector3 dirN, int lv, float speed, int hitcout)
    {
        ballH bullet = _pool[0].Reuse(pos);
        if (null != bullet)
        {
            bullet.transform.SetAngleOnXY_Fast(dirN);

            bullet.dir = dirN;
            bullet.speed = speed;
            bullet.lv = lv;
            bullet.draw.LineColor = colorDesign.ColorOfLv2(lv);
            bullet.dmg = ballDesign.DmgOfLv(lv);
            bullet.count = hitcout;
        }
        return bullet;
    }

}
