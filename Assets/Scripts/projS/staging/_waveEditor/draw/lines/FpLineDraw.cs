using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public class FpLineDraw : MonoBehaviour
{
    LineRenderer _renderer;
    float _depth;
    bool _isDashed = false;

    public bool IsDashed {get{ return _isDashed; } set { _isDashed = value; } }

    public void SetEndCap(int endCap)
    {
        _renderer.numCapVertices = endCap;
    }

    public void Init(LineRenderer renderer, float depth)
    {
        _renderer = renderer;
        _renderer.enabled = false;
        _depth = depth;
    }

    public void Active(Vector3 pos)
    {
        Active(pos, pos);
    }

    public void Active(Vector3 pos1, Vector3 pos2)
    {
        _renderer.enabled = true;
        _renderer.SetPositions(new Vector3[] { pos1.WithGapY(_depth), pos2.WithGapY(_depth) });
    }

    public void Inactive()
    {
        _renderer.enabled = false;
    }

    /*
    private void Update()
    {
        if (_isDashed)
            _renderer.material.SetFloat("Repeat Count", Mathf.Abs((_renderer.GetPosition(0) - _renderer.GetPosition(1)).magnitude) * 0.00007f);
    }
    */
}
