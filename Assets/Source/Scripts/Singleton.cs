using UnityEngine;

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T _instance;

    private static readonly object Lock = new object();

    [SerializeField] private bool _persistent;

    public static T Instance
    {
        get
        {
            if (Quitting)
                return null;

            lock (Lock)
            {
                if (_instance != null)
                    return _instance;

                T[] instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1)
                        return _instance = instances[0];
                    for (int i = 1; i < instances.Length; i++)
                        Destroy(instances[i].gameObject);

                    return _instance = instances[0];
                }

                return _instance = new GameObject().AddComponent<T>();
            }
        }
    }

    private void Awake()
    {
        if (_persistent)
            DontDestroyOnLoad(gameObject);

        OnAwake();
    }

    private void Start()
    {
        T[] instances = FindObjectsOfType<T>();
        var count = instances.Length;
        if (count > 0)
        {
            if (count == 1)
                return;

            for (int i = 0; i < instances.Length; i++)
                if(instances[i] != Instance)
                    Destroy(instances[i].gameObject);
        }
    }

    protected virtual void OnAwake() { }
}

public abstract class Singleton : MonoBehaviour
{
    public static bool Quitting { get; private set; }

    private void OnApplicationQuit()
    {
        Quitting = true;
    }
}
