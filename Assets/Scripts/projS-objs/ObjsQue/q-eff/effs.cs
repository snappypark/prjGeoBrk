using UnityEngine;
using UnityEngineEx;
using nj;

public partial class effs : ObjsQuePool<effs, eff>
{
    public const byte typeEdge = 0;
    public const byte typeBallEff = 1;
    public const byte typeStageComplemented = 2;
    public const byte typeHitMain = 3;
    public const byte typeLvUp = 4;
    public const byte numType = 5;
    short[] _capacits = new short[] { 96, 96, 4, 2, 1 };
    //    short[] _capacits = new short[] { 48, 96, 4, 2, 1};
    protected override short CapacityOfType(byte type) { return _capacits[type]; }

    protected override void _awake()
    {
        base._awake();
        for (int i = 0; i < numType; ++i)
            _tasks[i].Init(this);
    }
    
    public eff ExplosionEdge(Vector3 pos, Vector3 dir)
    {
        eff ef = _pool[typeEdge].Reuse(pos);
        if (null != ef)
        {
            ef.transform.SetAngleOnXY2(dir);
            _tasks[typeEdge].Add(Time.time + 1.7f, ef.cdx);
            for (int i = 0; i < ef.PS.Length; ++i)
                ef.PS[i].Play();
        }
        return ef;
    }

    public eff ExplosionBall(Vector3 pos, Color color, Vector3 dir)
    {
        eff ef = _pool[typeBallEff].Reuse(pos);
        if (null != ef)
        {
            ef.transform.SetAngleOnXY2(dir);
            for (int i = 0; i < ef.PS.Length; ++i)
            {
                ef.PS[i].Play();
                ParticleSystem.MainModule settings = ef.PS[i].main;
                settings.startColor = new ParticleSystem.MinMaxGradient(color);
            }
            _tasks[typeBallEff].Add(Time.time + 0.96f, ef.cdx);
        }
        return ef;
    }

    public eff Create(byte type, Vector3 pos )
    {
        eff ef = _pool[type].Reuse(pos);
        if (null != ef)
        {
            _tasks[type].Add(Time.time + 3.7f, ef.cdx);
            for (int i = 0; i < ef.PS.Length; ++i)
                ef.PS[i].Play();
        }
        return ef;
    }
}
