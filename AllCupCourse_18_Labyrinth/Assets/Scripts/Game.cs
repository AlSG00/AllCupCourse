using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Game : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private bool _isTimerOn;
    [SerializeField] private float _timerValue;
    [SerializeField] private TextMeshProUGUI _timerView;

    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private Exit _exitFromLevel;

    private float _timer = 0;
    private bool _isGameEnded = false;

    private void Awake()
    {
        _timer = _timerValue;
    }

    private void Start()
    {
        _exitFromLevel.Close();
    }

    private void Update()
    {
        if (_isGameEnded)
            return;

        TimerTick();
        LookAtPlayerHealth();
        LookAtPlayerInventory();
        TryCompleteLevel();
    }

    private void TimerTick()
    {
        if (_isTimerOn == false)
            return;

        _timer -= Time.deltaTime;
        _timerView.text = $"{_timer:F2}";

        if (_timer <= 0)
            Lose();
    }

    private void TryCompleteLevel()
    {
        if (_exitFromLevel.IsOpen == false)
            return;

        var flatExitPosition = new Vector2(_exitFromLevel.transform.position.x, _exitFromLevel.transform.position.z);
        var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

        if (flatExitPosition == flatPlayerPosition)
            Victory();
    }

    private void LookAtPlayerHealth()
    {
        if (_player.IsAlive)
            return;

        Lose();
        Destroy(_player.gameObject);
    }

    private void LookAtPlayerInventory()
    {
        if (_player.HasKey)
            _exitFromLevel.Open();
    }

    public void Victory()
    {
        _isGameEnded = true;
        _player.Disable();
        Debug.LogError("You win!");
    }

    public void Lose()
    {
        _isGameEnded = true;
        _player.Disable();
        Debug.LogError("You lose!");
    }

}
