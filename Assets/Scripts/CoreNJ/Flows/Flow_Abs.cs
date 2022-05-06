using System.Collections;
namespace nj
{ 
public abstract class Flow_Abs
{
    public int NextiType;
    public virtual int iType { get { return 0; } }

    public virtual void Update() { }
    public virtual IEnumerator OnEnter_() { yield return null; }
    public virtual IEnumerator OnUpdate_() { yield return null; }
    public virtual IEnumerator OnExit_() { yield return null; }
}
}