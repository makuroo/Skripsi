using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Card;
using Database;
using TMPro;

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
        [SerializeField] private TMP_Text _cardDescriptionText;
        [Space(5)]
        [SerializeField] private SerializedDictionary<State, DeckData> _deckDictionary;
        [Space(5)]
        [SerializeField] private List<CardDefinition> currentDeck = new();

        private CardTemplate _currentHand;
        
        public List<CardDefinition> CurrentDeck
        {
            get => currentDeck;
            set => currentDeck = value;
        }
        
        public static DeckManager Instance {get; private set; }

        private void OnEnable()
        {
            CardSwipe.OnUsedCard += UpdateDeck;
            GameManager.OnStageChange += ChangeDeck;
        }

        private void OnDisable()
        {
            CardSwipe.OnUsedCard -= UpdateDeck;
            GameManager.OnStageChange -= ChangeDeck;
        }

        private void Awake()
        {
            Instance = this;
            InitializeCurrentCard();
        }

        private void UpdateDeck(bool isSwipeLeft)
        {
            if (isSwipeLeft)
            {
                _currentHand.ActivateRightStrategy();
                Destroy(_currentHand);
            }
            else
            {
                _currentHand.ActivateLeftStrategy();
                Destroy(_currentHand);
            }
            
            CurrentDeck.RemoveAt(0);
            if (CurrentDeck.Count == 0)
            {
                GameManager.Instance.NextStage();
                return;
            }
            
            InitializeCurrentCard();
        }

        private void InitializeCurrentCard()
        {
            if (_currentHand == null)
            {
                CardTemplate template = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity, _templateParent);
                template.GetComponent<RectTransform>().anchoredPosition= Vector2.zero;
                template.Definition = CurrentDeck[0];
                template.Initialize();
                _cardDescriptionText.text = template.Definition.CardDescription;
                _currentHand = template;
            }
            else
            {
                _currentHand.Definition = CurrentDeck[0];
                _cardDescriptionText.text = _currentHand.Definition.CardDescription;
                _currentHand.Initialize();
            }
        }

        private void ChangeDeck(State _currentState)
        {
            var currDeckData = _deckDictionary[_currentState];
            
            List<CardDefinition> exceptionCards = new ();
            for (int i = 0; i < currDeckData.FixedCardsCount; i++)
            {
                exceptionCards.Add(Instantiate(currDeckData.FixedCardsList[i].Card));
            }
            
            var availableCards = GameDatabase.Instance.CardPool.Except(exceptionCards).ToList();
            CurrentDeck=availableCards.Take(currDeckData.TotalCards-currDeckData.FixedCardsCount).Distinct().ToList();
            for (int i = 0; i < currDeckData.FixedCardsCount; i++)
            {
                CurrentDeck.Insert(currDeckData.FixedCardsList[i].index,Instantiate(currDeckData.FixedCardsList[i].Card));
            }
            
            InitializeCurrentCard();
        }
    }
}