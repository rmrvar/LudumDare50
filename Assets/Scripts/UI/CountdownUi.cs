using System;
using TMPro;
using UnityEngine;

public class CountdownUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Countdown _countdown;
    public Countdown Countdown
    {
        get => _countdown;
        set
        {
            if (_countdown != null)
            {
                _countdown.OnTick -= OnTick;
            }
            _countdown = value;
            if (_countdown != null)
            {
                _countdown.OnTick += OnTick;
                OnTick();
            }
        }
    }

    private void OnTick()
    {
        Debug.Assert(_countdown != null, $"{gameObject.name} has null countdown!");

        _text.text = $"{Math.Ceiling(_countdown.Value)}";
    }
}
