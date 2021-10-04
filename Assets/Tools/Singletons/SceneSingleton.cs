using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    
    static T m_Instance;

    public static bool IsInstanceValid => m_Instance != null;
    public static T Instance => m_Instance;

    void Awake()
    {
        m_Instance = this as T;
    }
    
    void OnApplicationQuit()
    {
        m_Instance = null;
    }
 
    void OnDestroy()
    {
        m_Instance = null;
    }
    
}
