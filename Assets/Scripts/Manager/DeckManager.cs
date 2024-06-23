using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Card;
using Database;
using UnityEngine.Serialization;

namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    internal struct FixedCards
    {
        public CardDefinition Card;
        public int index;
    }

    [Serializable]
    struct DeckData
    {
        public int TotalCards;
        public int FixedCardsCount;
        public List<FixedCards> FixedCardsList;
    }
    
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private CardTemplate _cardPrefab;
        [SerializeField] private Transform _templateParent;

        [SerializeField] private SerializedDictionary<State, DeckData> _deckDictionary;
        
        [SerializeField] private List<CardDefinition> currentDeck = new();
        
        public List<CardDefinition> CurrentDeck
        {
            get => currentDeck;
            set => currentDeck = value;
        }
        
        public static DeckManager Instance {get; private set; }

        private void OnEnable()
        {
            CardInput.OnUsedCard += UpdateDeck;
            GameManager.OnStageChange += ChangeDeck;
        }

        private void OnDisable()
        {
            CardInput.OnUsedCard -= UpdateDeck;
            GameManager.OnStageChange -= ChangeDeck;
        }

        private void Awake()
        {
            Instance = this;
            InitializeCurrentCard();
        }

        private void UpdateDeck()
        {
            if(CurrentDeck.Count==0)
                GameManager.Instance.NextStage();
            
            CurrentDeck.RemoveAt(0);
            InitializeCurrentCard();
        }

        private void InitializeCurrentCard()
        {
            CardTemplate template = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity, _templateParent);
            template.GetComponent<RectTransform>().anchoredPosition= Vector2.zero;
            template.Definition = CurrentDeck[0];
            template.Initialize();
        }

        private void ChangeDeck(State _currentState)
        {
            var currDeckData = _deckDictionary[_currentState];
            
            List<CardDefinition> exceptionCards = new ();
            for (int i = 0; i < currDeckData.FixedCardsCount; i++)
            {
                exceptionCards.Add(currDeckData.FixedCardsList[i].Card);
            }
            
            var availableCards = GameDatabase.Instance.CardPool.Except(exceptionCards).ToList();
            CurrentDeck=availableCards.Take(currDeckData.TotalCards-currDeckData.FixedCardsCount).Distinct().ToList();
            for (int i = 0; i < currDeckData.FixedCardsCount; i++)
            {
                CurrentDeck.Insert(currDeckData.FixedCardsList[i].index,currDeckData.FixedCardsList[i].Card);
            }
        }
    }
}


