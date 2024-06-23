using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public enum State
    {
        MarketResearch=0,
        Initiation=1,
        PreProduction=2,
        Production=3,
        Alpha=4,
        Beta=5,
        Release=6
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
        private State _currentState;
        [SerializeField] 
        private SerializedDictionary<ScoresEnum, int> _scoreDictionary = new();
        public static GameManager Instance { get; private set; }
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
            _currentState++;
            OnStageChange?.Invoke(_currentState);
        }
    }
}
