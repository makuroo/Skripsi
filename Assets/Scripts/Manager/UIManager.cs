using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ScoresEnum, ScoreUI> _scoreUIDictionary = new();
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _playerWinPanel;
    public static Action<ScoresEnum, int> UpdateScoreUI;
    public static Action PlayerLose;
    public static Action PlayerWin;

    private void OnEnable()
    {
        UpdateScoreUI += OnUpdateScoreUI;
        PlayerWin += OnPlayerWin;
        PlayerLose += OnPlayerLose;
    }

    private void OnDisable()
    {
        UpdateScoreUI -= OnUpdateScoreUI;
        PlayerWin -= OnPlayerWin;
        PlayerLose -= OnPlayerLose;
    }

    private void OnUpdateScoreUI(ScoresEnum scoresEnum, int value)
    {
        if (_scoreUIDictionary.ContainsKey(scoresEnum))
        {
            _scoreUIDictionary[scoresEnum].UpdateUI(value);
        }
    }

    private void OnPlayerLose()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    private void OnPlayerWin()
    {
        Time.timeScale = 0;
        _playerWinPanel.SetActive(true);
    }
}
