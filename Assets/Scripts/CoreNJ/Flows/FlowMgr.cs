using System.Collections;

namespace nj
{ 
public class FlowMgr
{
    public int PreType;
    public int CurType { get { return _curFlow.iType; } }
    public int NextType { get { return _curFlow.NextiType; } }
    public Flow_Abs NextFlow { get { return _nextFlow; } }
    Flow_Abs _curFlow = null;
    Flow_Abs _nextFlow = null;

    public bool HasCurFlow { get { return _curFlow != null; } }

    bool _updateCoLoop = false;
    bool _flowLoop = false;

    public bool IsChanging { get { return !_flowLoop; } }

    public T Change<T>() where T : Flow_Abs, new()
    {
        _nextFlow = new T();
        if (_curFlow != null)
            _curFlow.NextiType = _nextFlow.iType;
        _flowLoop = false;
        return _nextFlow as T;
    }

    public void UpdateCurFlow()
    {
        _curFlow.Update();
    }

    public IEnumerator Update_()
    {
        _curFlow = _nextFlow;
        _curFlow.NextiType = _curFlow.iType;
        yield return null; yield return null;
        _updateCoLoop = true;
        while (_updateCoLoop)
        {
            _flowLoop = true;
            yield return _curFlow.OnEnter_();
            yield return null; yield return null; yield return null; yield return null;

            while (_flowLoop)
                yield return _curFlow.OnUpdate_();
            yield return null; yield return null; yield return null; yield return null;

            yield return _curFlow.OnExit_();

            PreType = _curFlow.iType;
            _curFlow = _nextFlow;
            _curFlow.NextiType = _curFlow.iType;
        }
    }

}
}