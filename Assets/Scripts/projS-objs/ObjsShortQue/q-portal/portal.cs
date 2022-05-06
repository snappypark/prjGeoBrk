using UnityEngine;
using UnityEngineEx;
using nj;

public class portal : sqObj
{
    [SerializeField] public CircleCollider2D collision;
    public float lifeTime;
    public portal Target = null;
    public short targetAdx;

    void Update()
    {
        
    }
}

public class OnPortal
{
    bool _moved = false;
    public bool Update(Transform tr)
    {
        portal foundPortal = null;
        portals.Inst.Query_IfTrueReturn(delegate (portal portal)
        {
            if (tr.localPosition.IsNearBy(
                portal.transform.localPosition,
                portal.collision.radius))
            {
                foundPortal = portal;
                return true;
            }
            return false;
        });

        if (foundPortal != null)
        {
            if (!_moved)
            {
                Vector3 gap = foundPortal.transform.localPosition - tr.localPosition;
                tr.localPosition 
                    = foundPortal.Target.transform.localPosition
                    - gap;
                _moved = true;
                return true;
            }
        }
        else
        {
            _moved = false;
        }
        return false;
    }
}