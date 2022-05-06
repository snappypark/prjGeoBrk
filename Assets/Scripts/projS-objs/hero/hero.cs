using UnityEngine;
using UnityEngineEx;
using nj;

public class hero : MonoSingleton<hero>
{
    [SerializeField] heroStats _stats = new heroStats();
    [SerializeField] heroMuzzles _muzzles = new heroMuzzles();
    [SerializeField] heroMove _move  = new heroMove();
    [SerializeField] public CircleCollider2D collision;

    [SerializeField] SpriteRenderer _spEff;
    public void Init()
    {
        gameObject.SetActive(true);
        _stats.SetByUserLv();
        _muzzles.SetByUserLv();
     //   UIs.Inst.Frame.RefreshLv();
    }

    void OnEnable()
    {
        _move.Init();
    }

    nj.skip collisionEdge = new nj.skip(6);
    nj.skip check = new nj.skip(5);
    void Update()
    {
        Vector3 curPos = transform.localPosition;

        edges.Inst.Query_EnqueOrDequeByReturn(curPos.Pt(), delegate (edge obj)
        {
            if (_stats.isHpZero)
                return ObjsPartialPool<edges, edge>.qReturn.Next;

            Vector3 nearestPos = curPos.NearestPos_OnLine(obj.Pos1, obj.Pos2);
            float dist = collision.radius + obj.collision.radius;

            if (curPos.IsFarFrom(nearestPos, dist))
                return ObjsPartialPool<edges, edge>.qReturn.Next;

            _stats.GotDamaged();
            obj.stats.Dead();

            if (obj.stats.isAlive)
                return ObjsPartialPool<edges, edge>.qReturn.Next_Exit;

            return ObjsPartialPool<edges, edge>.qReturn.Unuse_Exit;
        });

        _move.Update();

       // check.Code(() => {
       // });
        if (_stats.isHpZero)
        {
            hero.Inst.gameObject.SetActive(false);
            effs.Inst.Create(effs.typeHitMain, transform.localPosition);
            staging.Inst.SetStateOnPlay(staging.eState.over);
        }

    }

    ///*
    public void Move(Vector3 dir, bool bRotationed)
    {
        _move.SetTarget(dir, bRotationed);
    }//*/

    toNfro _vive = new toNfro(0.4f, 0.021f);
    public void RotateAndFire(Vector3 nDir_)
    {
        if (_stats.isHpZero)
            return;
        float scale = 0.70f+ _vive.GetDt();
        _spEff.transform.localScale = new Vector3(scale, scale, scale);
        _spEff.color = _spEff.color.Alpha(0.78f + scale*2.0f);

        _move.SetRotateTarget(nDir_);
        _muzzles.Fire(transform.localPosition, nDir_);

        //_move.SetTarget(nDir_, true);
    }

    public void ResetFire()
    {
        _spEff.color = _spEff.color.Alpha(0.52f);
        _muzzles.ResetTime();
    }

    public void GetExp()
    {
        _stats.GetExp();
        if (_stats.exp.isFull())
            LvUp();
    }

    public void LvUp()
    {
        User.Inst.CurLv.SetValue(Mathf.Clamp(User.Inst.CurLv + 1, 1, lvDesign.LastLv));
        User.Inst.CurExp = 0;
        _stats.SetByUserLv();
        _muzzles.SetByUserLv();
        effs.Inst.Create(effs.typeLvUp, transform.localPosition);
        UIs.Inst.Hud.LvUp.Show();
        UIs.Inst.Frame.RefreshLv();
    }

}
