using UnityEngine;
using UnityEngineEx;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public partial class waveEditor /*State Action*/
{
    public enum eState
    {
        None = 0,
        EdgeDot,
       // Portal,
        SetLv,
        Delete,
    }

    eState _state = eState.None;

    public void SetState(eState state)
    {
        switch (state)
        {
            case eState.None:
                whatLvEdge = 1;
                whatLvDot = 1;
                hasStartDot = true;
                hasEndDot = true;
                isStraght = true;
                break;
        }
        _state = state;
        _touchReset(false);
    }

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        switch (_state)
        {
            case eState.EdgeDot:
                OnUpdate_OnEdgeDot();
                break;
         //   case eState.Portal:
          //      OnUpdate_OnPortalOpen();
           //     break;
            case eState.SetLv:
                OnUpdate_OnSetLv();
                break;
            case eState.Delete:
                OnUpdate_OnDelete();
                break;
        }
    }

    #region OnUpdate_OnEdgeDot
    List<edge> _edges = new List<edge>();
    dot _beginDot = null;
    dot _endDot = null;
    void OnUpdate_OnEdgeDot()
    {
        _drawHoverCircle();

        if (_touch.Plan_End(MOUSE_LEFT))
            if (_addEndTouchPos())
                _addEdges_continue();

        if (_touch.Plan_End(MOUSE_RIGHT))
            if (_addEndTouchPos())
            {
                _addEdges_end();
                _touchReset();
            }
    }

    void _addEdges_continue()
    {
        // begin to add edge
        if (_touchList.Count == 1)
        {
            _edges.Clear();
            _beginDot = dots.Inst.FindByPos(_touchlstBegin);
            if (_beginDot == null)
                _beginDot = dots.Inst.Spawn(
                    whatLvDot, _touchlstBegin);
        }
        else
            _addEdgesByList();
    }

    void _addEdges_end()
    {
        _addEdgesByList();

        _endDot = dots.Inst.FindByPos(_touchlstEnd);
        if (_endDot == null)
            _endDot = dots.Inst.Spawn(
                whatLvDot, _touchlstEnd);

        if (_beginDot != null && _endDot != null)
            _LINK_DOTS(_beginDot, _endDot, _edges);

        // generate graphs
        _generateGraphs();
    }

    void _addEdgesByList()
    {
        if (_touchList.Count < 2)
            return;

        Vector3 begin = _touchList[_touchList.Count - 2];
        Vector3 end = _touchList[_touchList.Count - 1];
        dot beginDot = dots.Inst.FindByPos(begin);
        dot endDot = dots.Inst.FindByPos(end);
        if (beginDot != null)
            begin = beginDot.transform.localPosition;
        if (endDot != null)
            end = endDot.transform.localPosition;

        Vector3 v = end - begin;
        Vector3 nv = v.normalized;
        float dist = v.magnitude;

        if (!isStraght)
        {
            dist = Mathf.Min(dist, 0.9999f);
            _touchList[_touchList.Count - 1] = begin + nv * dist;
        }

        int numLine = (int)dist + 1;
        float eachLength = dist / (float)numLine;

        float gap = 0.0f;// 0.034f 0.02f;
        Vector3 v2 = nv * eachLength;

        for (int i = 0; i < numLine; ++i)
        {
            _edges.Add(
                edges.Inst.Spawn(
                    whatLvEdge,
                    begin + v2 * i - nv * gap,
                    begin + v2 * (i + 1) + nv * gap));
        }
    }
    
    static void _LINK_DOTS(dot d1, dot d2, List<edge> edgs)
    {
        if (edgs.Count == 0)
            return;
        int num = edgs.Count;
        halfEdge half1 = d1.AddHalf(true,
            edgs[0].transform.NorVecFrom(d1.transform));
        halfEdge half2 = d2.AddHalf(false,
            edgs[num - 1].transform.NorVecFrom(d2.transform));
        half1.pair = half2;
        half2.pair = half1;

        for (int i = 0; i < num; ++i)
        {
            edgs[i].SetHalf(half1);
            half1.cdxs.Push(edgs[i].cdx);
        }
    }
    #endregion

    #region OnUpdate_OnPortalOpen
    void OnUpdate_OnPortalOpen()
    {
        _drawHoverCircle();

        if (_touch.Plan_End(MOUSE_LEFT))
        {
            portals.Inst.Open(portals.eType.tunnel, _touch.EndPos, 0);
            _touchReset();
        }
    }
    #endregion

    #region OnUpdate_OnSetLv
    void OnUpdate_OnSetLv()
    {
        _drawHoverCircle();

        if (_touch.Plan_End(MOUSE_LEFT))
        {
            dot d = dots.Inst.FindByPos(_touch.EndPos);
            if (d != null)
                d.AssignByLv(whatLvEdge);

            edge e = edges.Inst.FindByPos(_touch.EndPos);
            if (e != null)
                e.stats.Init(whatLvEdge);
            _touchReset();
        }
    }
    #endregion

    #region OnUpdate_OnDelete
    void OnUpdate_OnDelete()
    {
        _drawHoverCircle();

        if (_touch.Plan_End(MOUSE_LEFT))
        {
            dot d = dots.Inst.FindByPos(_touch.EndPos);
            if (d != null)
                dots.Inst.Unuse(d);

            edge e = edges.Inst.FindByPos(_touch.EndPos);
            if (e != null)
                edges.Inst.Unuse(e);

            portals.Inst.Query_IfTrueReturn(delegate (portal portal)
            {
                if (_touch.EndPos.IsNearBy(portal.transform.localPosition, portal.collision.radius))
                {
                    portals.Inst.Close(portal);
                    return true;
                }
                return false;
            });

            _generateGraphs();
            _touchReset();
        }
    }
    #endregion
}
