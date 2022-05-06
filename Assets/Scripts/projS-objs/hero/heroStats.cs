using UnityEngine;

[System.Serializable]
public class heroStats
{
    [SerializeField] heroHud _hud = new heroHud();

    [HideInInspector] public rInt hp;
    [HideInInspector] public rInt exp;
    [HideInInspector] public bool isHpZero = false;

    public void SetByUserLv()
    {
        hp = new rInt(lvDesign.GetMaxLife());
        exp = new rInt(User.Inst.CurExp, lvDesign.GetMaxExp());
        _hud.RefreshHp(hp);
        _hud.RefreshExp(exp);
        isHpZero = false;
    }

    public void GotDamaged()
    {
        if (isHpZero)
            return;
        hp.decreaseClamp();
        _hud.RefreshHp(hp);
        if (hp.isZero())
            isHpZero = true;
    }

    public void GetExp()
    {
        exp.increaseClamp();
        _hud.RefreshExp(exp);
        User.Inst.CurExp.SetValue(exp);
    }
}
