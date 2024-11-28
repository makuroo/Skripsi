using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [Header("Main Menu")] 
        [SerializeField] private List<Sprite> _tutorialSprites = new();
        [SerializeField] private Image _tutorialImage;
        [SerializeField] private GameObject _howToPlayPanel;
        private int _index;
        
        [Space(5)]
        [SerializeField] private SerializedDictionary<ScoresEnum, ScoreUI> _scoreUIDictionary = new();
        [Header("LosePanel")]
        [Space(5)]
        [SerializeField] private GameObject _gameOverPanel;
        
        [Header("WinPanel")]
        [Space(5)]
        [SerializeField] private GameObject _playerWinPanel;
        [SerializeField] private TMP_Text _ratingScoreWin;
        [SerializeField] private TMP_Text _incomeScoreWin;
        [SerializeField] private TMP_Text _gradeText;

        [Header("Game Phase")] 
        [SerializeField] private TMP_Text _currentPhaseUI; 
        
        public static Action<ScoresEnum, int> UpdateScoreUI;
        public static Action PlayerLose;
        public static Action PlayerWin;
    
        private void OnEnable()
        {
            UpdateScoreUI += OnUpdateScoreUI;
            PlayerWin += OnPlayerWin;
            PlayerLose += OnPlayerLose;
            GameManager.OnStateChange += OnUpdateCurrentPhaseText;
        }
    
        private void OnDisable()
        {
            UpdateScoreUI -= OnUpdateScoreUI;
            PlayerWin -= OnPlayerWin;
            PlayerLose -= OnPlayerLose;
            GameManager.OnStateChange -= OnUpdateCurrentPhaseText;
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
            _ratingScoreWin.text = GameManager.Instance.RatingScore.ToString();
            _incomeScoreWin.text = GameManager.Instance.IncomeScore.ToString();
            _gradeText.text = GameManager.Instance.CalculateGrade().ToString();
            _playerWinPanel.SetActive(true);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
            #endif
            Application.Quit();
        }

        public void HowToPlay()
        {
            _index = 0;
            _howToPlayPanel.SetActive(!_howToPlayPanel.activeSelf);
        }

        public void Next()
        {
            if (_index == _tutorialSprites.Count - 1)
            {
                return;
            }

            _index++;
            _tutorialImage.sprite = _tutorialSprites[_index];
        }
        
        public void Previous()
        {
            if (_index == 0)
            {
                return;
            }

            _index--;
            _tutorialImage.sprite = _tutorialSprites[_index];
        }

        private void OnUpdateCurrentPhaseText(State state)
        {
            _currentPhaseUI.text = state.ToString();
        }
    }
}

