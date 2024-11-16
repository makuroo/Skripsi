using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ScoresEnum, ScoreUI> _scoreUIDictionary = new();
    [Header("LosePanel")]
    [Space(5)]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _ratingScoreLose;
    [SerializeField] private TMP_Text _incomeScoreLose;
    
    [Header("WinPanel")]
    [Space(5)]
    [SerializeField] private GameObject _playerWinPanel;
    [SerializeField] private TMP_Text _ratingScoreWin;
    [SerializeField] private TMP_Text _incomeScoreWin;
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
        _ratingScoreLose.text = GameManager.Instance.RatingScore.ToString();
        _incomeScoreLose.text = GameManager.Instance.IncomeScore.ToString();
        _gameOverPanel.SetActive(true);
    }

    private void OnPlayerWin()
    {
        Time.timeScale = 0;
        _ratingScoreWin.text = GameManager.Instance.RatingScore.ToString();
        _incomeScoreWin.text = GameManager.Instance.IncomeScore.ToString();
        _playerWinPanel.SetActive(true);
    }
}
