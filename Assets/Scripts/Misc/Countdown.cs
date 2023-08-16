using System;

public class Countdown
{
    public bool IsPaused { get; private set; } = true;

    public float InitialValue { get; private set; }

    public float Value { get; private set; }
    public float TickProgress { get; private set; }

    public event Action OnTick;
    public event Action OnCompleted;

    public Countdown(float initialValue)
    {
        Reset(initialValue);
    }

    public void Reset(float initialValue = -1)
    {
        if (initialValue >= 0)
        {
            InitialValue = initialValue;
        }
        Value = InitialValue;
        IsPaused = true;
    }

    public void Start()
    {
        IsPaused = false;
    }

    public void Pause()
    {
        IsPaused = true;
    }

    public void Update(float dt)
    {
        if (IsPaused)
        {
            return;
        }

        Value -= dt;
        TickProgress += dt;

        while (TickProgress > 1)
        {
            OnTick?.Invoke();
            TickProgress -= 1;
        }

        if (Value <= 0)
        {
            OnCompleted?.Invoke();
        } 
    }
}
