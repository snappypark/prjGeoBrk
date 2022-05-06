using UnityEngine;

public static class stageFrame
{
    //public static Vector3 center = new Vector3(centerX, centerY);
    //public const float centerX = 7.0f;
    //public const float centerY = 12.5f;

    public const float left = 1.25f;
    public const float right = 12.85f;

    public const float top = 20.9f;
    public const float bottom = 4.35f;


    public const float leftOut = 0.35f;
    public const float rightOut = 13.65f;

    public const float topOut = 21.65f;
    public const float bottomOut = 2.85f;

    public static Rect GetByHalfSize(float halfW, float halfH)
    {
        return Rect.MinMaxRect(left + halfW,
                                bottom + halfH,
                                right - halfW,
                                top - halfH);
    }

    public static bool IsOutFrmae(Vector3 pos)
    {
        return pos.y > topOut || pos.x > rightOut ||
            pos.x < leftOut || pos.y < bottomOut;
    }
}
