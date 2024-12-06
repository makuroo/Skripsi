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
        [SerializeField] private List<GameObject> _tutorialGameObjects = new();
        [SerializeField] private Image _tutorialImage;
        [SerializeField] private GameObject _howToPlayPanel;
        private int _index;
        
        [Space(5)]
        [SerializeField] private SerializedDictionary<ScoresEnum, ScoreUI> _scoreUIDictionary = new();
        [SerializeField] private SerializedDictionary<ScoresEnum, Image> _scoreIndicatorDictionary = new();
        
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

        public static Action<ScoresEnum, float> UpdateScoreIndicatorAlpha;

        public SerializedDictionary<ScoresEnum, Image> ScoreIndicatorDictionary => _scoreIndicatorDictionary;

        private void OnEnable()
        {
            UpdateScoreUI += OnUpdateScoreUI;
            PlayerWin += OnPlayerWin;
            PlayerLose += OnPlayerLose;
            UpdateScoreIndicatorAlpha += OnUpdateScoreIndicatorAlpha;
            GameManager.OnStateChange += OnUpdateCurrentPhaseText;
        }
    
        private void OnDisable()
        {
            UpdateScoreUI -= OnUpdateScoreUI;
            PlayerWin -= OnPlayerWin;
            PlayerLose -= OnPlayerLose;
            UpdateScoreIndicatorAlpha -= OnUpdateScoreIndicatorAlpha;
            GameManager.OnStateChange -= OnUpdateCurrentPhaseText;
        }

        private void Start()
        {
            foreach (var i in _scoreIndicatorDictionary.Values)
            {
                i.color = new Color(1, 1, 1, 0);
            }
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
            foreach (var go in _tutorialGameObjects)
            {
                go.SetActive(false);
            }
            _tutorialGameObjects[0].SetActive(true);
            _howToPlayPanel.SetActive(!_howToPlayPanel.activeSelf);
        }

        public void Next()
        {
            if (_index == _tutorialGameObjects.Count - 1)
            {
                return;
            }
            _tutorialGameObjects[_index].SetActive(false);
            _index++;
            _tutorialGameObjects[_index].SetActive(true);
        }
        
        public void Previous()
        {
            if (_index == 0)
            {
                return;
            }

            _tutorialGameObjects[_index].SetActive(false);
            _index--;
            _tutorialGameObjects[_index].SetActive(true);
        }

        public void CloseHowToPlay()
        {
            _howToPlayPanel.SetActive(false);
        }

        private void OnUpdateCurrentPhaseText(State state)
        {
            switch (state)
            {
                case State.Intro:
                    _currentPhaseUI.text = "Introduction";
                    break;
                case State.MarketResearch:
                    _currentPhaseUI.text = "Market Research";
                    break;
                case State.Initiation:
                    _currentPhaseUI.text = "Initiation";
                    break;
                case State.PreProduction:
                    _currentPhaseUI.text = "Pre-Production";
                    break;
                case State.Production:
                    _currentPhaseUI.text = "Production";
                    break;
                case State.Testing:
                    _currentPhaseUI.text = "Testing";
                    break;
                case State.Beta:
                    _currentPhaseUI.text = "Beta";
                    break;
                case State.Release:
                    _currentPhaseUI.text = "Release";
                    break;
                case State.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
           
        }

        private void OnUpdateScoreIndicatorAlpha(ScoresEnum scoresEnum, float alphaAmount)
        {
            var color = ScoreIndicatorDictionary[scoresEnum].color;
            color.a = alphaAmount;
            ScoreIndicatorDictionary[scoresEnum].color = color;  
        }
    }
}

