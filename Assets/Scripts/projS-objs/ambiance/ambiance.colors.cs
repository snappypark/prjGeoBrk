using UnityEngine;

public partial class ambiance /*colors*/
{

    Color _curCol1, _curCol2, _curCol3, _curCol4;
    Color _nextCol1, _nextCol2, _nextCol3, _nextCol4;

    void SetNextToCurColor()
    {
        _nextCol1 = _curCol1;
        _nextCol2 = _curCol2;
        _nextCol3 = _curCol3;
        _nextCol4 = _curCol4;
    }

    bool IsChanged()
    {
        Color gap = _curCol1 - _nextCol1;
        return gap.r > 0.01f || gap.g > 0.01f || gap.b > 0.01f;
    }

    const int numColor = 39;
    bool _changed = false;
    void SetCurColors(int idx)
    {
        switch (idx)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                _curCol1 = new Color(0.018f, 0.049f, 0.132f);
                _curCol2 = new Color(0.005f, 0.107f, 0.198f);
                _curCol3 = new Color(0.020f, 0.216f, 0.386f);
                _curCol4 = new Color(0.173f, 0.522f, 0.518f);
                return;
            case 4:
            case 5:
            case 6:
                _curCol1 = new Color(0.035f, 0.104f, 0.061f);
                _curCol2 = new Color(0.049f, 0.144f, 0.160f);
                _curCol3 = new Color(0.208f, 0.306f, 0.376f);
                _curCol4 = new Color(0.310f, 0.435f, 0.376f);
                return;

            case 7:
            case 8:
            case 9:
            case 10:
                _curCol1 = new Color(0.134f, 0.151f, 0.130f);
                _curCol2 = new Color(0.139f, 0.170f, 0.169f);
                _curCol3 = new Color(0.145f, 0.424f, 0.361f);
                _curCol4 = new Color(0.202f, 0.173f, 0.518f);
                return;
            case 11:
            case 12:
            case 13:
                _curCol1 = new Color(0.053f, 0.063f, 0.094f);
                _curCol2 = new Color(0.110f, 0.140f, 0.188f);
                _curCol3 = new Color(0.082f, 0.482f, 0.431f);
                _curCol4 = new Color(0.467f, 0.204f, 0.565f);
                return;

            case 14:
            case 15:
            case 16:
            case 17:
                _curCol1 = new Color(0.119f, 0.225f, 0.245f);
                _curCol2 = new Color(0.046f, 0.165f, 0.236f);
                _curCol3 = new Color(0.051f, 0.114f, 0.227f);
                _curCol4 = new Color(0.608f, 0.643f, 0.239f);
                return;
            case 18:
            case 19:
            case 20:
                _curCol1 = new Color(0.053f, 0.113f, 0.108f);
                _curCol2 = new Color(0.191f, 0.217f, 0.169f);
                _curCol3 = new Color(0.424f, 0.278f, 0.267f);
                _curCol4 = new Color(0.839f, 0.906f, 0.604f);
                return;

            case 21:
            case 22:
            case 23:
            case 24:
                _curCol1 = new Color(0.102f, 0.080f, 0.067f);
                _curCol2 = new Color(0.189f, 0.123f, 0.070f);
                _curCol3 = new Color(0.482f, 0.440f, 0.082f);
                _curCol4 = new Color(0.244f, 0.173f, 0.518f);
                return;
            case 25:
            case 26:
            case 27:
                _curCol1 = new Color(0.065f, 0.062f, 0.123f);
                _curCol2 = new Color(0.226f, 0.130f, 0.110f);
                _curCol3 = new Color(0.453f, 0.445f, 0.000f);
                _curCol4 = new Color(0.641f, 0.037f, 0.000f);
                return;

            case 28:
            case 29:
            case 30:
            case 31:
                _curCol1 = new Color(0.075f, 0.035f, 0.041f);
                _curCol2 = new Color(0.075f, 0.075f, 0.179f);
                _curCol3 = new Color(0.059f, 0.000f, 0.455f);
                _curCol4 = new Color(0.631f, 0.502f, 0.255f);
                return;
            case 32:
            case 33:
            case 34:
                _curCol1 = new Color(0.116f, 0.072f, 0.198f);
                _curCol2 = new Color(0.097f, 0.048f, 0.198f);
                _curCol3 = new Color(0.314f, 0.369f, 0.094f);
                _curCol4 = new Color(0.161f, 0.659f, 0.153f);
                return;

            case 35:
            case 36:
            case 37:
            case 38:
                _curCol1 = new Color(0.027f, 0.056f, 0.188f);
                _curCol2 = new Color(0.059f, 0.047f, 0.200f);
                _curCol3 = new Color(0.463f, 0.529f, 0.086f);
                _curCol4 = new Color(0.129f, 0.533f, 0.706f);
                return;
            case 39:
            case 40:
            case 41:
                _curCol1 = new Color(0.027f, 0.056f, 0.188f);
                _curCol2 = new Color(0.031f, 0.097f, 0.161f);
                _curCol3 = new Color(0.329f, 0.584f, 0.263f);
                _curCol4 = new Color(0.518f, 0.490f, 0.145f);
                return;


            default:
                _curCol1 = new Color(0.018f, 0.049f, 0.132f);
                _curCol2 = new Color(0.005f, 0.107f, 0.198f);
                _curCol3 = new Color(0.020f, 0.216f, 0.386f);
                _curCol4 = new Color(0.173f, 0.522f, 0.518f);
                return;

        }
    }


    void SetCurColors_(string tag)
    {
        switch (tag)
        {
            case "winter,night":
                _curCol1 = new Color(0.098f, 0.110f, 0.114f);
                _curCol2 = new Color(0.108f, 0.135f, 0.160f);
                _curCol3 = new Color(0.307f, 0.380f, 0.528f);
                _curCol4 = new Color(0.268f, 0.291f, 0.321f);
                return;

            case "blue":
                _curCol1 = new Color(0.018f, 0.049f, 0.132f);
                _curCol2 = new Color(0.023f, 0.119f, 0.216f);
                _curCol3 = new Color(0.020f, 0.216f, 0.386f);
                _curCol4 = new Color(0.000f, 0.433f, 0.429f);
                return;

            case "green,sky":
                _curCol1 = new Color(0.092f, 0.151f, 0.094f);
                _curCol2 = new Color(0.051f, 0.198f, 0.127f);
                _curCol3 = new Color(0.084f, 0.231f, 0.481f);
                _curCol4 = new Color(0.161f, 0.264f, 0.216f);
                return;

            case "green,sky1":
                _curCol1 = new Color(0.117f, 0.132f, 0.114f);
                _curCol2 = new Color(0.147f, 0.186f, 0.189f);
                _curCol3 = new Color(0.164f, 0.491f, 0.417f);
                _curCol4 = new Color(0.202f, 0.173f, 0.518f);
                return;

            case "green,sky2":
                _curCol1 = new Color(0.059f, 0.099f, 0.151f);
                _curCol2 = new Color(0.059f, 0.175f, 0.247f);
                _curCol3 = new Color(0.207f, 0.386f, 0.472f);
                _curCol4 = new Color(0.779f, 0.840f, 0.131f);
                return;

            case "snow,sky,green":
                _curCol1 = new Color(0.065f, 0.104f, 0.101f);
                _curCol2 = new Color(0.111f, 0.164f, 0.189f);
                _curCol3 = new Color(0.082f, 0.482f, 0.470f);
                _curCol4 = new Color(0.244f, 0.173f, 0.518f);
                return;

            case "coler, red":
                _curCol1 = new Color(0.075f, 0.035f, 0.041f);
                _curCol2 = new Color(0.170f, 0.028f, 0.065f);
                _curCol3 = new Color(0.453f, 0.445f, 0.000f);
                _curCol4 = new Color(0.641f, 0.037f, 0.000f);
                return;

            case "wood":
                _curCol1 = new Color(0.102f, 0.080f, 0.067f);
                _curCol2 = new Color(0.189f, 0.123f, 0.070f);
                _curCol3 = new Color(0.482f, 0.440f, 0.082f);
                _curCol4 = new Color(0.244f, 0.173f, 0.518f);
                return;

            case "purple":
                _curCol1 = new Color(0.102f, 0.067f, 0.085f);
                _curCol2 = new Color(0.120f, 0.055f, 0.180f);
                _curCol3 = new Color(0.217f, 0.082f, 0.482f);
                _curCol4 = new Color(0.351f, 0.173f, 0.518f);
                return;
                

            default:
                _curCol1 = new Color(0.075f, 0.035f, 0.041f);
                _curCol2 = new Color(0.170f, 0.028f, 0.065f);
                _curCol3 = new Color(0.453f, 0.445f, 0.000f);
                _curCol4 = new Color(0.641f, 0.037f, 0.000f);
                return;

        }
    }
}
