using UnityEngine;
using UnityEngineEx;
using UnityEngine.EventSystems;
using System.Collections.Generic;

using nj;
public partial class waveEditor // TouchInfo
{
    [SerializeField] Transform _transMarkHover;

    TouchInfo _touch = new TouchInfo();
    List<Vector3> _touchList = new List<Vector3>();
    Vector3 _touchlstBegin { get { return _touchList[0]; } }
    Vector3 _touchlstEnd { get { return _touchList[_touchList.Count - 1]; }}

    bool _addEndTouchPos()
    {
        int numPos = _touchList.Count;
        if (numPos == 0)
            _touchList.Add(_touch.EndPos);
        else if (_touchList[numPos - 1].IsFarFrom(_touch.EndPos, 0.2f))
            _touchList.Add(_touch.EndPos);
        return numPos < _touchList.Count;
    }
    
    void _touchReset(bool includeDraw = true)
    {
        _transMarkHover.gameObject.SetActive(false);
        _touchList.Clear();
    }

    void _drawHoverCircle()
    {
        if (_touch.Is_PlanHovering)
        {
            _transMarkHover.gameObject.SetActive(dots.Inst.FindByPos(_touch.HoverPos) != null);
            _transMarkHover.transform.localPosition = _touch.HoverPos;
        }
    }

    class TouchInfo
    {
        public bool IsActing = false;
        public Vector3 HoverPos;
        public Vector3 DragGap { get { return EndPos - BeginPos; } }
        public Vector3 BeginPos;
        public Vector3 EndPos;

        public bool Plan_Begin { get { return TouchPlan_Begin(ref BeginPos) && !EventSystem.current.IsPointerOverGameObject(); } }
        public bool Plan_Pressed { get { return TouchPlan_Pressed(ref EndPos); } }
        public bool Plan_End(int lefOrRight = MOUSE_LEFT) { return TouchPlan_End(ref EndPos, lefOrRight); }
        public bool Is_PlanHovering { get { return PickingPlan(out HoverPos); } }
    }
    
    #region touch plan
    const int MOUSE_LEFT = 0;
    const int MOUSE_RIGHT = 1;

    public static bool TouchPlan_Begin(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButtonDown(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool TouchPlan_Pressed(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButton(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool TouchPlan_End(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButtonUp(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool PickingPlan(out Vector3 touchedPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, AppLayer.TouchPlanMask))
        {
            touchedPos = new Vector3(hit.point.x, hit.point.y);
            return true;
        }
        touchedPos = Vector3.positiveInfinity;
        return false;
    }
    #endregion
}