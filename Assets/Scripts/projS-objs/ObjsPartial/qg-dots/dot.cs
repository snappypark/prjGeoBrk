using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;
using VolumetricLines;
using nj;

public partial class dot : pqObj
{
    [SerializeField] public VolumetricLineBehavior draw;
    [SerializeField] public CircleCollider2D collision;
    
    public byte Lv = 0;
    
    public override void OnClone()
    {
        _state = eState.waiting;
    }

    static Queue<dot> _nearDots = new Queue<dot>();
    public override void OnUnuse(eUnuse type_ = eUnuse.Default)
    {
        int numHalf = halfs.Count;
        for (int i = 0; i < numHalf; ++i)
        {
            halfEdge haf = halfs[i];
            if(haf.pair != null)
                _nearDots.Enqueue(haf.pair.origin);
            haf.Clear(transform.localPosition);
        }
        
        //graph
        if (_nearDots.Count > 0)
        {
            dot d1 = _nearDots.Dequeue();
            rDots.Refresh(d1);

            bool seperate = false;
            int numNearDots = _nearDots.Count;
            for (int i = 0; i < numNearDots; ++i)
            {
                dot d2 = _nearDots.Dequeue();
                if (d1.rPreToken == d2.rCurToken)
                {
                    seperate = true;
                    rDots.RefreshNew(d2);
                    d2.rdot.SetNextTypeBySplit(transform.localPosition);
                }
            }
            if (seperate)
            {
                d1.rdot.SetNextTypeBySplit(transform.localPosition);
            }
        }

        halfs.Clear();
        _state = eState.waiting;
        // effs.Inst.Create(effs.typeBallEff, transform.localPosition);
    }

    public toNfro tnf;// = new toNfro(1, 0.2f);
    void Update()
    {
#if UNITY_EDITOR
        if (App.Inst.FlowMgr.CurType == (int)ProjS.Flow.eType.EditWave)
        {
            draw.SetStartAndEndPoints(new Vector3(-0.0001f, 0, 2), new Vector3(0.0001f, 0, 2));
            return;
        }
#endif
        //float gapScale =  0.54f - tnf.GetDt();
        //transform.localScale = new Vector3(gapScale, gapScale, 1);
        /*
        switch (_state)
        {
            case eState.waiting:
                if (waiting.IsEnd())
                {
                    spawnning.Reset();
                    _state = eState.spwanning;
                }
                break;
            case eState.spwanning:
                if (spawnning.IsEnd())
                {
                    draw.SetStartAndEndPoints(new Vector3(-0.0001f, 0, 2), new Vector3(0.0001f, 0, 2));
                    _state = eState.none;
                }
                break;
            default:
                break;
        }
        */
        dots.Inst.Move(this);

        switch (rdot.numDots)
        {
            case 0:
                rdot.numDots = 0;
                dots.Inst.Unuse(this);
                break;
            case 1:
                rdot.numDots = 0;
                dots.Inst.Unuse(this);
                break;
            default:
                transform.localPosition = rdot.rootPos + rVector;
                break;
        }
    }
    
    
    #region Color, Hp table
    public void AssignByLv(byte lv)
    {
        Lv = lv;
        draw.LineColor = _colorOfLv(Lv);
        draw.LineWidth = 0.010f + 0.003f * Lv;// 0.025f;
    }

    static Color _colorOfLv(int lv)
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
    
    #endregion
    
}
