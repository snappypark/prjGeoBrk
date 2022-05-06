using System.Collections;
using UnityEngine;
using UnityEngineEx;
using nj;

public struct CamOrthInfo
{
    public Vector3 Pos;
    public float OrthSize;
    public CamOrthInfo(Vector3 pos, float orthSize)
    {
        Pos = pos;
        OrthSize = orthSize;
    }
}

public class cams : MonoSingleton<cams>
{
    [SerializeField] AniCurveEx _moveAni;
    [SerializeField] public Camera Main;

    public void SetOrthInfo(CamOrthInfo info)
    {
        Main.transform.localPosition = info.Pos;
        Main.orthographicSize = info.OrthSize;
    }

    public IEnumerator SetOrthInfo_(float time, CamOrthInfo info)
    {
        _moveAni.ResetTime(time);
        while (_moveAni.UpdateUntilTime())
        {
            float t = _moveAni.Evaluate();
            Main.transform.localPosition
                = Vector3.Lerp(Main.transform.localPosition, info.Pos, t);
            Main.orthographicSize
                = Mathf.Lerp(Main.orthographicSize, info.OrthSize, t);
            yield return null;
        }
        SetOrthInfo(info);
        yield return null;
    }


}
