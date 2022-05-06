using UnityEngine;
using VolumetricLines;

[System.Serializable]
public class edgeStats
{
    [SerializeField] VolumetricLineBehavior _hud;
    delay _hudDelay = new delay(0.06f);

    public byte Lv = 0;
    public int Hp = 160;
    public bool isAlive = true;

    [HideInInspector] float width;
    public void Init(byte lv)
    {
        Lv = lv;
        Hp = edgeDesign.HpOfLv(Lv);
        isAlive = true;
        RefreshHud();
    }

    public void GotDamaged(int dmg)
    {
        Hp -= dmg;
        if (Hp < 1)
        {
            --Lv;
            Hp = edgeDesign.HpOfLv(Lv);
            RefreshHud();
            isAlive = Hp > 0;
        }

        if (_hudDelay.IsEnd())
            _hudDelay.Reset();
        _hud.LightSaberFactor = 0.1f;
    }

    public void Dead()
    {
        Hp = 0;
        isAlive = false;
    }

    public void ResetHudLightSaber(float newWidthGap=0)
    {
        _hud.LineWidth = width;// - newWidthGap;
        if (_hudDelay.afterOnceTime())
            _hud.LightSaberFactor = 0.88f;
    }

    public void RefreshHud()
    {
        _hud.LineColor = colorDesign.ColorOfLv(Lv);
        _hud.LineWidth = 0.01639f;// + 0.0003f * Lv;// 0.025f;
        width = _hud.LineWidth;
        _hud.LightSaberFactor = 0.88f;//Lv >= 8 ? 0.82f : 1;
    }
}
