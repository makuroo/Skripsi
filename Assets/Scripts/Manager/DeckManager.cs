using System;
using Card;

namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private CardTemplate _cardPrefab;
        [SerializeField] private Transform _templateParent;
        [SerializeField] private List<CardDefinition> currentCurrentDeck = new();
        
        public List<CardDefinition> CurrentDeck
        {
            get => currentCurrentDeck;
            set => currentCurrentDeck = value;
        }
        
        public static DeckManager Instance {get; private set; }

        private void OnEnable()
        {
            CardInput.OnUsedCard += UpdateDeck;
        }

        private void OnDisable()
        {
            CardInput.OnUsedCard -= UpdateDeck;
        }

        private void Awake()
        {
            Instance = this;
            
            CardTemplate template = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity, _templateParent);
            template.GetComponent<RectTransform>().anchoredPosition= Vector2.zero;
            template.Definition = CurrentDeck[0];
            template.Initialize();
        }

        private void UpdateDeck()
        {
            CurrentDeck.RemoveAt(0);
            CardTemplate template = Instantiate(_cardPrefab, Vector3.zero, Quaternion.identity, _templateParent);
            template.GetComponent<RectTransform>().anchoredPosition= Vector2.zero;
            template.Definition = CurrentDeck[0];
            template.Initialize();
        }
    }
}


