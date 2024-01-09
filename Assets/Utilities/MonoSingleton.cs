using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType<T>();
            }
            return instance;
        }

    }

    void Awake()
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        if (global) DontDestroyOnLoad(this.gameObject);
        this.OnAwake();
    }

    protected virtual void OnAwake()
    {

    }
}