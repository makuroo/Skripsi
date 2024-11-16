using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ScoresEnum, ScoreUI> _scoreUIDictionary = new();
    public static Action<ScoresEnum, int> UpdateScoreUI;

    private void OnEnable()
    {
        UpdateScoreUI += OnUpdateScoreUI;
    }

    private void OnDisable()
    {
        UpdateScoreUI -= OnUpdateScoreUI;
    }

    private void OnUpdateScoreUI(ScoresEnum scoresEnum, int value)
    {
        if (_scoreUIDictionary.ContainsKey(scoresEnum))
        {
            _scoreUIDictionary[scoresEnum].UpdateUI(value);
        }
    }
}
