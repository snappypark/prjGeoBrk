using UnityEngine;

public partial class muzzleDesign
{
    public static int Set(ref muzzle[] mz_)
    {
        if (User.Inst.CurLv <= lvDesign.LastLv)
            return _set_lv_0_120(ref mz_);
        return 0;
    }

    static int _set_lv_0_120(ref muzzle[] mz_)
    {
        int lv = User.Inst.CurLv;
        int i;
        switch (lv)
        {
            #region lv 1 - 12
            case 1:
                mz_[0] = new muzzle(1, 1, d10, s10, cc0, rc0);
                mz_[1] = new muzzle(1, 1, d01, s14, cc1, rc3); return 2;
            case 2:
                mz_[0] = new muzzle(1, 1, d01, s20, cc0, rc0);
                mz_[1] = new muzzle(1, 1, d11, s13, cc1, rc2);
                mz_[2] = new muzzle(1, 1, d12, s14, cc2, rc2); return 3;
            case 3:
            case 4:
                i = lv - 3;
                mz_[0] = new muzzle(1, 2, d0[i], s0[i], cc0, rc0);
                mz_[1] = new muzzle(1, 1, d1[i], s1[i], cc1, rc1);
                mz_[2] = new muzzle(1, 1, d2[i], s2[i], cc2, rc2);
                mz_[3] = new muzzle(1, 1, d3[i], s3[i], cc3, rc3); return 4;
            case 5:
            case 6:
            case 7:
                i = lv - 5;
                mz_[0] = new muzzle(1, 2, d0[i], s0[i], cc0, rc0);
                mz_[1] = new muzzle(1, 2, d1[i], s1[i], cc1, rc1);
                mz_[2] = new muzzle(1, 1, d2[i], s2[i], cc2, rc2, 2);
                mz_[3] = new muzzle(1, 1, d3[i], s3[i], cc3, rc3, 2); return 4;
               // mz_[4] = new muzzle(1, 1, d4[i], s4[i], cc4, rc4, 2); return 4;
            case 8:
            case 9:
            case 10:
                i = lv - 8;
                mz_[0] = new muzzle(1, 2, d0[i], s0[i], cc0, rc0, (1 + i) % 4);
                mz_[1] = new muzzle(1, 2, d1[i], s1[i], cc1, rc1, (1 + i) % 3);
                mz_[2] = new muzzle(1, 2, d2[i], s2[i], cc2, rc2, 3);
                mz_[3] = new muzzle(1, 1, d3[i], s3[i], cc3, rc3, 3);
                mz_[4] = new muzzle(1, 1, d4[i], s4[i], cc4, rc4, 2); return 4;
            //mz_[5] = new muzzle(1, 1, d5[i], s5[i], cc5, rc5, 1);
            #endregion

            default:
                i = (lv - 3) >> 1;
                int m = Mathf.Clamp(55 - i, 6, 56);
                int n = (int)(m / 6);
                int i0 = Mathf.Clamp((int)((i + 5) / n), 0, 27);
                int i1 = Mathf.Clamp((int)((i + 4) / n), 0, 20);
                int i2 = Mathf.Clamp((int)((i + 3) / n), 0, 14);
                int i3 = Mathf.Clamp((int)((i + 2) / n), 0, 9);
                int i4 = Mathf.Clamp((int)((i + 1) / n), 0, 5);
                //int i5 = Mathf.Clamp((int)((i + 1) / n), 0, 2);
                float f = lv * 0.0015f;
                float f2 = f*2f;
                //Debug.Log("" + f); // 120 -> 0.204
                int hitcnt0 = (1 + i) % 4;
                int hitcnt1 = (1 + i) % 3;
                mz_[0] = new muzzle(alv[i0], anv[i0], d0[0] - f, s0[0] + f2, cc0, rc0, hitcnt0);
                mz_[1] = new muzzle(alv[i1], anv[i1], d1[0] - f, s1[0] + f2, cc1, rc1, 3);
                mz_[2] = new muzzle(alv[i2], anv[i2], d2[0] - f, s2[0] + f2, cc2, rc2, 3);
                mz_[3] = new muzzle(alv[i3], anv[i3], d3[0] - f, s3[0] + f2, cc3, rc3, 2);
                //mz_[4] = new muzzle(alv[i4], anv[i4], d4[0] - f, s4[0] + f2, cc4, rc4, 2);
                //mz_[5] = new muzzle(alv[i5], anv[i5], d5[0] - f, s5[0] + f, cc5, rc5, 1);
               // mz_[6] = new muzzle(alv[0], anv[0], d6[0] - f, s6[0] + f, cc6, rc6);
                return 4;
        }
    }
}
