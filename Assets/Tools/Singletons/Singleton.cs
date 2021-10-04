using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    
    static bool m_ShuttingDown = false;
    static object m_Lock = new object();
    static T m_Instance;

    public static bool IsInstanceValid => !m_ShuttingDown && m_Instance != null;
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }
 
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    // Search for Resources prefab
                    if (m_Instance == null)
                    {
                        GameObject singletonPrefab = (GameObject)Resources.Load(typeof(T).ToString(), typeof(GameObject));
                        if (singletonPrefab != null)
                        {
                            GameObject singletonObject = Instantiate(singletonPrefab);
                            m_Instance = singletonObject.GetComponent<T>();
                            singletonObject.name = $"{typeof(T)} (Singleton)";
 
                            // Make instance persistent.
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                    
                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        GameObject singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T)} (Singleton)";
 
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
 
                return m_Instance;
            }
        }
    }
 
    void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }
 
    void OnDestroy()
    {
        m_ShuttingDown = true;
    }
    
}