using UnityEngine;
using nj;

[System.Serializable]
public class heroHud
{
    [SerializeField] Transform _tranHp;
    [SerializeField] Transform _tranExp;
    
    public void RefreshHp(rInt hp_)
    {
        _tranHp.localScale = new Vector3(hp_.Ratio01(), _tranHp.localScale.y, 1);
    }

    public void RefreshExp(rInt exp_)
    {
        _tranExp.localScale = new Vector3(exp_.Ratio01(), _tranExp.localScale.y, 1);
    }

}
