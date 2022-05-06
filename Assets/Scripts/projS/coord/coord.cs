using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using nj;
public static class coord
{
    public static CamOrthInfo CamOrthInfo_Play { get { return new CamOrthInfo(new Vector3(7, 13 - Mathf.Max(0, (12 * AppScr.AspectH_W - 18) * 0.5f), -100), 6.0f * AppScr.AspectH_W); } }

    public static readonly Vector2 overSize = new Vector2(0.0714285714285714f, 0.0384615384615385f);
    public static readonly Pt size = new Pt(14, 26);
    public static readonly Pt sizeHalf = new Pt(7, 13);

    public const float moveLimit_minX = 1.2f;
    public const float moveLimit_maxX = 12.8f;
    public const float moveLimit_minY = 1.2f;
    public const float moveLimit_maxY = 24.8f;


    public const float partialLimit_minX = 0.1f;
    public const float partialLimit_maxX = 13.9f;
    public static float partialLimit_minY ;
    public static float partialLimit_maxY ;

    public static void Awake()
    {
        //partialLimit_minY = sizeHalf.y - sizeHalf.x * AppScr.AspectH_W + 1.7f;
        //partialLimit_maxY = sizeHalf.y + sizeHalf.x * AppScr.AspectH_W - 1.7f;
        partialLimit_minY = 4 + 0.3f;
        partialLimit_maxY = 22 - 0.3f;
    }

    public static bool IsOutOfPos_OnPartial(Vector3 pos)
    {
        return pos.x < partialLimit_minX
            || pos.x > partialLimit_maxX
            || pos.y < partialLimit_minY
            || pos.y > partialLimit_maxY;
    }
    
    public static bool IsOutOfIdx_OnPartial(Pt pt)
    {
        return pt.x < 0 || pt.x >= size.x
            || pt.y < 0 || pt.y >= size.y;
    }

    public static bool IsInPartial(Pt pt)
    {
        return pt.x >= 0 && pt.x < size.x
            && pt.y >= 0 && pt.y < size.y;
    }

    public static Vector3 CutBoundary(Vector3 pos)
    {
        return new Vector3(
                Mathf.Clamp(pos.x, coord.moveLimit_minX, coord.moveLimit_maxX),
                Mathf.Clamp(pos.y, coord.partialLimit_minY, coord.partialLimit_maxY)
                );
    }
}
