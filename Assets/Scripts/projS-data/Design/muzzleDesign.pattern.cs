using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class muzzleDesign /*pattern */
{
    const int maxalv = 28;
    static int[] alv = new int[] { 1, 1,2, 1,2,3, 1,2,3,4, 1,2,3,4,5, 1,2,3,4,5,6, 1,2,3,4,5,6,7};
    static int[] anv = new int[] { 1, 2,1, 3,2,1, 4,3,2,1, 5,4,3,2,1, 6,5,4,3,2,1, 7,6,5,4,3,2,1};

    #region common
    const float cc0 = 1;  const float rc0 = 0.0f;
    const float cc1 = -1; const float rc1 = 0.03f;
    const float cc2 = 1;  const float rc2 = 0.056f;
    const float cc3 = -1; const float rc3 = 0.085f;
    const float cc4 = 1;  const float rc4 = 0.11f;
    const float cc5 = -1; const float rc5 = 0.135f;
    const float cc6 = 1;  const float rc6 = 0.151f;
    #endregion

    static float[] d0 = new float[] { d00, d10, d20};
    static float[] d1 = new float[] { d01, d11, d21};
    static float[] d2 = new float[] { d02, d12, d22};
    static float[] d3 = new float[] { d03, d13, d23};
    static float[] d4 = new float[] { d04, d14, d24};
  //  static float[] d5 = new float[] { d05, d15, d25};
  //  static float[] d6 = new float[] { d06, d16, d26};

    static float[] s0 = new float[] { s00, s10, s20};
    static float[] s1 = new float[] { s01, s11, s21};
    static float[] s2 = new float[] { s02, s12, s22};
    static float[] s3 = new float[] { s03, s13, s23};
    static float[] s4 = new float[] { s04, s14, s24};
    //  static float[] s5 = new float[] { s05, s15, s25};
    //  static float[] s6 = new float[] { s06, s16, s26};


    #region 0
    const float d00 = 0.13f; const float s00 = 10.4f;
    const float d01 = 0.20f; const float s01 = 9.7f;
    const float d02 = 0.22f; const float s02 = 9.6f;
    const float d03 = 0.24f; const float s03 = 9.5f;
    const float d04 = 0.26f; const float s04 = 9.4f;
    //   const float d05 = 0.34f; const float s05 = 10.3f;
    //   const float d06 = 0.38f; const float s06 = 10.2f;
    #endregion

    #region 1
    const float d10 = 0.11f; const float s10 = 10.4f;
    const float d11 = 0.15f; const float s11 = 9.6f;
    const float d12 = 0.18f; const float s12 = 9.5f;
    const float d13 = 0.23f; const float s13 = 8.7f;
    const float d14 = 0.16f; const float s14 = 8.6f;
    //   const float d15 = 0.33f; const float s15 = 9.5f;
    //   const float d16 = 0.25f; const float s16 = 9.4f;
    #endregion

    #region 2
    const float d20 = 0.10f; const float s20 = 11.2f;
    const float d21 = 0.14f; const float s21 = 10.9f;
    const float d22 = 0.17f; const float s22 = 10.8f;
    const float d23 = 0.19f; const float s23 = 9.6f;
    const float d24 = 0.16f; const float s24 = 9.5f;
    //   const float d25 = 0.33f; const float s25 = 9.9f;
    //   const float d26 = 0.22f; const float s26 = 9.8f;
    #endregion

    /*
#region 0
const float d00 = 0.23f; const float s00 = 10.4f;
const float d01 = 0.30f; const float s01 = 9.7f;
const float d02 = 0.32f; const float s02 = 9.6f;
const float d03 = 0.34f; const float s03 = 9.5f;
const float d04 = 0.36f; const float s04 = 9.4f;
//   const float d05 = 0.34f; const float s05 = 10.3f;
//   const float d06 = 0.38f; const float s06 = 10.2f;
#endregion

#region 1
const float d10 = 0.21f; const float s10 = 10.4f;
const float d11 = 0.25f; const float s11 = 9.6f;
const float d12 = 0.28f; const float s12 = 9.5f;
const float d13 = 0.33f; const float s13 = 8.7f;
const float d14 = 0.26f; const float s14 = 8.6f;
//   const float d15 = 0.33f; const float s15 = 9.5f;
//   const float d16 = 0.25f; const float s16 = 9.4f;
#endregion

#region 2
const float d20 = 0.20f; const float s20 = 11.2f;
const float d21 = 0.24f; const float s21 = 10.9f;
const float d22 = 0.27f; const float s22 = 10.8f;
const float d23 = 0.29f; const float s23 = 9.6f;
const float d24 = 0.26f; const float s24 = 9.5f;
//   const float d25 = 0.33f; const float s25 = 9.9f;
//   const float d26 = 0.22f; const float s26 = 9.8f;
#endregion
*/
}
