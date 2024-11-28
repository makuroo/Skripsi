using System;
using System.Diagnostics;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Manager
{
    public enum State
    {
        Intro = 0,
        MarketResearch=1,
        Initiation=2,
        PreProduction=3,
        Production=4,
        Alpha=5,
        Beta=6,
        Release=7,
        None=99
    }

    public enum ScoresEnum
    {
        ArtistBurnout,
        DesignerBurnout,
        ProgrammerBurnout,
        SoundEngineerBurnout,
        VisualScore,
        GameplayScore,
        TechnicalScore,
        AudioScore,
        MonetizationScore,
        MarketingScore,
        RatingScore,
        IncomeScore,
        FundScore
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private State _currentState;
        [SerializeField] 
        private SerializedDictionary<ScoresEnum, int> _scoreDictionary = new();
        public static GameManager Instance { get; private set; }

        [SerializeField] private int _ratingScore;
        [SerializeField] private int _incomeScore; 

        public State CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        public int RatingScore => _ratingScore;
        public int IncomeScore => _incomeScore;

        public static Action<State> OnStateChange;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if(Instance!=null && Instance!=this)
                Destroy(gameObject);
        }

        private void Start()
        {
            Time.timeScale = 1;
            foreach (var key in _scoreDictionary.Keys)
            {
                UIManager.UpdateScoreUI?.Invoke(key, _scoreDictionary[key]);
            }
        }

        public void UpdateScoreDictionary(SerializedDictionary<ScoresEnum, int> changes)
        {
            foreach (var key in changes.Keys.ToList())
            {
                _scoreDictionary[key] += changes[key];
                UIManager.UpdateScoreUI?.Invoke(key,_scoreDictionary[key]);
            }

            _ratingScore = CalculateRatingScore();
            _incomeScore = CalculateIncomeScore();

            if (IsLost())
            {
                UIManager.PlayerLose?.Invoke();
            }
        }

        public void NextStage()
        {
            
            if (CurrentState == State.Release)
            {
                UIManager.PlayerWin?.Invoke();
                return;
            }

            CurrentState++;
            OnStateChange?.Invoke(CurrentState);
        }

        private int CalculateRatingScore() =>
            (_scoreDictionary[ScoresEnum.VisualScore] +
             _scoreDictionary[ScoresEnum.GameplayScore] +
             _scoreDictionary[ScoresEnum.TechnicalScore] +
             _scoreDictionary[ScoresEnum.AudioScore]) / 4;

        private int CalculateIncomeScore() =>
            (int) (.4f * _scoreDictionary[ScoresEnum.MonetizationScore]) +
            (int)(.4f* _scoreDictionary[ScoresEnum.MarketingScore]) + (int)(.2f * RatingScore);

        private bool IsLost() => _scoreDictionary[ScoresEnum.FundScore] <= 0 ||
                                _scoreDictionary[ScoresEnum.ArtistBurnout] >= 100 ||
                                _scoreDictionary[ScoresEnum.ProgrammerBurnout] >= 100 ||
                                _scoreDictionary[ScoresEnum.DesignerBurnout] >= 100 ||
                                _scoreDictionary[ScoresEnum.SoundEngineerBurnout] >= 100;

        public char CalculateGrade()
        {
            return (int)((_incomeScore * .5 + _ratingScore * .5) / 2) switch
            {
                > 95 => 'S',
                >= 90 => 'A',
                >= 80 => 'B',
                >= 70 => 'C',
                >= 50 => 'D',
                < 50 => 'F'
            };
        }
    }
}
