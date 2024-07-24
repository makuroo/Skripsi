using System;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Card;
using Database;
using TMPro;
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
        [FormerlySerializedAs("index")] public int Index;
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
            ChangeDeck(GameManager.Instance.CurrentState);
            InitializeCurrentCard();
        }

        private void UpdateDeck(bool isSwipeLeft)
        {
            if (isSwipeLeft)
            {
                if(_currentHand.Definition.RightEffect)
                    _currentHand.ActivateRightStrategy();
            }
            else
            {
                if(_currentHand.Definition.LeftEffect)
                    _currentHand.ActivateLeftStrategy();
            }
            Destroy(_currentHand);
            
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
            var availableCards = GameDatabase.Instance.CardPoolDictionary[_currentState].Except(exceptionCards).ToList();
            CurrentDeck=availableCards.Take(currDeckData.TotalCards-currDeckData.FixedCardsCount).Distinct().ToList();
            for (int i = 0; i < currDeckData.FixedCardsCount; i++)
            {
                CurrentDeck.Insert(currDeckData.FixedCardsList[i].Index,Instantiate(currDeckData.FixedCardsList[i].Card));
            }
            
            InitializeCurrentCard();
        }
        public void InsertCardToDeck(State targetStateDeck,int index, CardDefinition card)
        {
            if (targetStateDeck == State.None)
            {
                currentDeck.Insert(index,card);
            }
            else
            {
                var newFixedCard = new FixedCards
                {
                    Index = index,
                    Card = Instantiate(card)
                };
                _deckDictionary[targetStateDeck].FixedCardsList.Add(newFixedCard);
            }
        }
    }
}