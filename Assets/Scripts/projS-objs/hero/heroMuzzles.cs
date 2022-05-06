using UnityEngine;

public class heroMuzzles
{
    muzzle[] _muzzles = new muzzle[11] {
        new muzzle(), new muzzle(), new muzzle(),
        new muzzle(), new muzzle(), new muzzle(), new muzzle(),
        new muzzle(), new muzzle(), new muzzle(), new muzzle(),};
    toNfro _bf = new toNfro(9.9f, 2.0f);
    int _num = 0;

    public void Fire(Vector3 heroPos, Vector3 nDir_)
    {
        Vector3 firePos = heroPos;// + nDir * 0.5f;
        Vector3 cross = Vector3.Cross(nDir_, Vector3.forward) * _bf.GetDt();

        for (int i = 0; i < _num; ++i)
            _muzzles[i].Fire(firePos, nDir_, cross);
    }

    public void ResetTime()
    {
        for (int i = 0; i < _num; ++i)
            _muzzles[i].ResetTime();
    }

    public void SetByUserLv()
    {
        _num = muzzleDesign.Set(ref _muzzles);
    }
}
