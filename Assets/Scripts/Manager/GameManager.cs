using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

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
        Fund
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private State _currentState;
        [SerializeField] 
        private SerializedDictionary<ScoresEnum, int> _scoreDictionary = new();
        public static GameManager Instance { get; private set; }

        public State CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        public static Action<State> OnStageChange;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if(Instance!=null && Instance!=this)
                Destroy(gameObject);
        }

        public void UpdateScoreDictionary(SerializedDictionary<ScoresEnum, int> changes)
        {
            foreach (var key in changes.Keys.ToList())
            {
                _scoreDictionary[key] += changes[key];
            }
        }

        public void NextStage()
        {
            if (CurrentState == State.Release) return;

            CurrentState++;
            OnStageChange?.Invoke(CurrentState);
        }
    }
}
