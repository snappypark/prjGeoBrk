using UnityEngine;

public static class colorDesign
{
    public static Color ColorOfLv(int lv)
    {
        switch (lv)
        {
            case 1: return new Color(0.50f, 0.50f, 0.50f);
            case 2: return new Color(0.50f, 0.50f, 0.30f);
            case 3: return new Color(0.20f, 0.55f, 0.20f);

            case 4: return new Color(0.20f, 0.60f, 0.50f);

            case 5: return new Color(0.20f, 0.33f, 0.75f);

            case 6: return new Color(0.33f, 0.10f, 0.64f);
            case 7: return new Color(0.65f, 0.10f, 0.35f);

            case 8: return new Color(1, 1, 0);
            case 9: return new Color(0, 1, 1);
            case 10: return new Color(1, 0, 1);
            default: return new Color(0.0f, 0.0f, 0.0f);
        }
    }

    public static Color ColorOfLv2(int lv)
    {
        switch (lv)
        {
            case 1: return new Color(0.40f, 0.50f, 0.60f);
            case 2: return new Color(0.40f, 0.50f, 0.40f);
            case 3: return new Color(0.15f, 0.55f, 0.40f);

            case 4: return new Color(0.10f, 0.60f, 0.55f);

            case 5: return new Color(0.20f, 0.35f, 0.75f);

            case 6: return new Color(0.33f, 0.15f, 0.70f);
            case 7: return new Color(0.80f, 0.15f, 0.40f);

            case 8: return new Color(1, 1, 0);
            case 9: return new Color(0, 1, 1);
            case 10: return new Color(1, 0, 1);
            default: return new Color(0.0f, 0.0f, 0.0f);
        }
    }
}
