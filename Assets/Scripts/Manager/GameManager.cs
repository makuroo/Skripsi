using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Manager
{
    public enum State
    {
        MarketResearch,
        Initiation,
        PreProduction,
        Production,
        Alpha,
        Beta,
        Release
    }

    public enum ScoresEnum
    {
        Burnout,
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
    }
}
