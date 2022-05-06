using UnityEngine;

namespace nj
{ 
public abstract class Singleton<T> where T : Singleton<T>, new()
{
    protected static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
                _inst = new T();
            return _inst;
        }
    }
    public static bool Exists
    {
        get
        {
            return _inst != null;
        }
    }
}

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_inst;

    private static object m_sLock = new object();

    private static bool m_sAppIsQuitting = false;

    public static T InstWithNoCreate
    {
        get
        {
            return m_inst;
        }
    }

    public static T Inst
    {
        get
        {
            if (m_sAppIsQuitting)
                return null;

            lock (m_sLock)
            {
                if (m_inst == null)
                {
                    m_inst = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return m_inst;
                    }

                    if (m_inst == null)
                    {
                        GameObject singleton = new GameObject();
                        m_inst = singleton.AddComponent<T>();
                        singleton.name = "(Singleton)" + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                    else
                    {
                    }
                }

                return m_inst;
            }
        }
    }

    public void OnDestroy()
    {
        m_sAppIsQuitting = true;
    }
}
}