using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idx
{
    static int[] _IdxGap3x3_I = new int[9] {
            0, // 중앙먼저 시작
            -1, -1, -1,  0, 0,  1, 1, 1,  };

    static int[] _IdxGap3x3_J = new int[9] {
            0, // 중앙먼저 시작
            -1, 0, 1,  -1, 1,  -1, 0, 1,  };

    public static int I3x3(int idx)
    {
        return _IdxGap3x3_I[idx];
    }
    public static int J3x3(int idx)
    {
        return _IdxGap3x3_J[idx];
    }

    public static Pt Pt3x3(int idx)
    {
        return new Pt(_IdxGap3x3_I[idx], _IdxGap3x3_J[idx]);
    }
}
