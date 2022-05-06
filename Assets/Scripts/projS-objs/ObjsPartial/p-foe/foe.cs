using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;
using VolumetricLines;
using nj;

public class foe : pqObj
{
    [SerializeField] public VolumetricLineBehavior draw;
    [SerializeField] public CircleCollider2D collision;
    [SerializeField] public int sideMoveType = 0;
    [SerializeField] public float speed = 1.0f;


    public byte Lv = 0;
    public int Hp;

    private void Awake()
    {
        sideMoveType = Random.Range(0, 2);
    }
    
    OnPortal _onPortal = new OnPortal();
    private void Update()
    {
#if UNITY_EDITOR
        if (App.Inst.FlowMgr.CurType == (int)ProjS.Flow.eType.EditWave)
            return;
#endif

        foes.Inst.Move(this);
    }

    #region Color, Hp table
    public void AssignByLv(byte lv)
    {
        Lv = lv;
        Hp = HpOfLv(Lv);
        draw.LineColor = ColorOfLv(Lv);
        //draw.LineWidth = -0.03f - 0.0035f * Lv;// 0.025f;
    }

    public static Color ColorOfLv(int lv)
    {
        switch (lv)
        {
            case 1: return new Color(0.55f, 0.55f, 0.55f);
            case 2: return new Color(0.75f, 0.75f, 0.0f);
            case 3: return new Color(0.1f, 0.75f, 0.1f);

            case 4: return new Color(0.0f, 0.65f, 0.4f);

            case 5: return new Color(0.0f, 0.25f, 0.85f);

            case 6: return new Color(0.45f, 0.0f, 1.0f);
            case 7: return new Color(1.0f, 0.0f, 0.45f);

            case 8: return new Color(1, 1, 0);
            case 9: return new Color(0, 1, 1);
            case 10: return new Color(1, 0, 1);
            default:
                break;
        }
        return new Color(0.55f, 0.55f, 0.55f);
    }

    public static int HpOfLv(int lv)
    {
        switch (lv)
        {
            case 1: return 10;
            case 2: return 20;
            case 3: return 40;

            case 4: return 80;
            case 5: return 160;
            case 6: return 320;
            case 7: return 640;

            case 8: return 9999;
            case 9: return 99999;
            case 10: return 999999;
            default:
                break;
        }
        return 9999999;
    }
    #endregion
}
