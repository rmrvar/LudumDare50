using UnityEngine;

public class PlayManager : MonoSingleton<PlayManager>
{
    [SerializeField]
    public float _gameLength;

    private Countdown _countdown;
    [SerializeField]
    private CountdownUi _countdownUi;

    protected override void SingletonAwake()
    {
        _countdown = new Countdown(_gameLength);
        _countdown.OnCompleted += OnCountdownCompleted;
    }

    protected override void SingletonStart()
    {
        _countdownUi.Countdown = _countdown;
    }

    protected override void SingletonUpdate()
    {
        _countdown.Update(Time.deltaTime);
    }

    protected override void SingletonOnDestroy()
    {
        _countdown.OnCompleted -= OnCountdownCompleted;
    }

    private void OnCountdownCompleted()
    {
        // TODO: Trigger the win menu.
        MenuManager.Instance.LoadWinMenu();
    }

    public void StartGameplay()
    {
        _countdown.Start();

        var spawner = GameObject.FindObjectOfType<SpawnOnTimer>();
        if (spawner)
        {
            spawner.enabled = true;
        } else
        {
            Debug.LogError("Missing SpawnOnTimer object!");
        }

        // This destroys the other tutorial pickup choice.
        var pickups = FindObjectsOfType<UpgradePickup>();
        foreach (var pickup in pickups)
        {
            Destroy(pickup.gameObject);
        }
    }
}
