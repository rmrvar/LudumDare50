using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance { get; private set; }

    private bool _wasKilled = false;

    #region ---- Unity Events ----

    // The reason I am hiding the base Unity functions this way is because I 
    // want to guarantee that these functions will only be called if the 
    // newly created MonoSingleton instance was not immediately destroyed.

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError($"Another instance of MonoSingleton ${typeof(T).Name} already exists!");
            SingletonKill();
            _wasKilled = true;
        }
        else
        {
            Instance = (T) this;
        }

        if (!_wasKilled)
        {
            SingletonAwake();
        }
    }

    private void Start()
    {
        if (!_wasKilled)
        {
            SingletonStart();
        }
    }

    private void OnEnable()
    {
        if (!_wasKilled)
        {
            SingletonOnEnable();
        }
    }

    private void OnDisable()
    {
        if (!_wasKilled)
        {
            SingletonOnDisable();
        }
    }

    private void OnDestroy()
    {
        if (!_wasKilled)
        {
            SingletonOnDestroy();
        }
    }

    private void Update()
    {
        if (!_wasKilled)
        {
            SingletonUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (!_wasKilled)
        {
            SingletonFixedUpdate();
        }
    }

    #endregion

    #region ---- Singleton Events ----

    // Override to change how the new MonoSingleton instance is destroyed.
    protected virtual void SingletonKill()
    {
        Destroy(this);
    }

    protected virtual void SingletonAwake() { }
    protected virtual void SingletonStart() { }
    protected virtual void SingletonOnEnable() { }
    protected virtual void SingletonOnDisable() { }
    protected virtual void SingletonOnDestroy() { }
    protected virtual void SingletonUpdate() { }
    protected virtual void SingletonFixedUpdate() { }

    // TODO: Add the other events as they are needed.

    #endregion
}
