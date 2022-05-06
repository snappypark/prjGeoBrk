using UnityEngine;
using UnityEngineEx;

[System.Serializable]
public class heroMove
{
    [SerializeField] Transform _tran;
    [SerializeField] Transform _tranRotate;

    [SerializeField] float _moveSpeed = 10.0f;
    [SerializeField] VecDamp _vecDamp_Move = new VecDamp();
    [SerializeField] VecDamp _vecDamp_Rotate = new VecDamp();
    [SerializeField] VecDamp _vecDamp_Rotate2 = new VecDamp();
    
    public Vector3 CurDir { get { return _vecDamp_Rotate2.Target; } }

    public void Init()
    {
        //_tran.localPosition = initPos;
        _tranRotate.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
        _vecDamp_Move.Init(_tran.localPosition);
        _vecDamp_Rotate.Init(new Vector3(0, 1));
        _vecDamp_Rotate2.Init(new Vector3(0, 1));
    }

    OnPortal _onPortal = new OnPortal();
    public void Update()
    {
        if (_vecDamp_Move.UpdateUntilCoincide())
        {
            Vector3 nextPos = coord.CutBoundary(_vecDamp_Move.Cur);
            _tran.localPosition = nextPos;
        }

      //  if (_onPortal.Update(_tran))
      //      _vecDamp_Move.Init(_tran.localPosition);

        ///*
        if (_vecDamp_Rotate.UpdateUntilCoincide())
        {
            _tranRotate.SetAngleOnXY(_vecDamp_Rotate.Cur);
            _vecDamp_Rotate2.Init(_vecDamp_Rotate.Cur);
        }
        else if (_vecDamp_Rotate2.UpdateUntilCoincide())
        {
            _tranRotate.SetAngleOnXY(_vecDamp_Rotate2.Cur);
        }//*/
    }

    public void SetTarget(Vector3 nDir_, bool bRotationed)
    {
        Vector3 targetPos = _tran.localPosition - nDir_ * _moveSpeed * Time.smoothDeltaTime;

        _vecDamp_Move.SetNewTarget(targetPos);
      //  _vecDamp_Move.SetSmoothTime(0.1f + 0.1f * (Vector3.Dot(nDir_, _vecDamp_Rotate2.Cur) - 1));

        //  Vector3 nextPos = coord.CutBoundary(targetPos);
        //_tran.localPosition = coord.CutBoundary(targetPos);

        if (!bRotationed)
            _vecDamp_Rotate2.SetNewTarget(nDir_);
    }
    public Vector3 nDir;
    public void SetRotateTarget(Vector3 nDir_)
    {
        //_vecDamp_Rotate.SetNewTarget(nDir);
        nDir = nDir_;
        _tranRotate.SetAngleOnXY(nDir_);
    }
}
