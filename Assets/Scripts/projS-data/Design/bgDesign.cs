using UnityEngine;

public static class bgDesign
{
    public static Color initColor = new Color(0, 0.05f, bgColor.max);
    public static bgColor initBgColor 
        = new bgColor(bgColor.eState.forth, initColor, 1);
}

public struct bgColor
{
    public const float max = 0.1f;
    const float _gap = 0.01f;
    public enum eState
    {
        back,
        forth,
    }

    public eState state;
    public int rgbIdx;
    float[] _color;

    public bgColor(eState state_, Color color_, int rgbIdx_ = 1)
    {
        state = state_;
        rgbIdx = rgbIdx_;
        _color = new float[3] { color_.r, color_.g, color_.b };
    }

    public float R { get { return _color[0]; } }
    public float G { get { return _color[1]; } }
    public float B { get { return _color[2]; } }

    public Color GetCurColor()
    {
        return new Color(_color[0], _color[1], _color[2]);
    }

    public Color GetNextColor()
    {
        float cur = _color[rgbIdx];
        switch (state)
        {
            case eState.back:
                cur -= _gap;
                if (cur < -0.00001f)
                {
                    state = eState.forth;
                    rgbIdx = (rgbIdx + 1) % 3;
                    cur = _color[rgbIdx];
                    switch (rgbIdx)
                    {
                        case 0:
                            cur = max;
                            break;
                        case 2:
                            cur = max * 0.5f;
                            break;
                    }
                }
                break;
            case eState.forth:
                cur += _gap;
                if (cur > max)
                {
                    state = eState.back;
                    rgbIdx = (rgbIdx + 1) % 3;
                    cur = _color[rgbIdx];
                    switch (rgbIdx)
                    {
                        case 1:
                            cur = max * 0.5f;
                            break;
                    }
                }
                break;
        }
        _color[rgbIdx] = Mathf.Clamp(cur, 0, max);
        return new Color(_color[0], _color[1], _color[2]);
    }

}