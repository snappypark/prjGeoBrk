using UnityEngine;

[ExecuteInEditMode]
public class AppEditor : MonoBehaviour
{
  //  [SerializeField] stageBg _bg = new stageBg();
#if UNITY_EDITOR
    /*
    void Update()
    {
        _bg.Update();
    }*/
#else
#endif
}
