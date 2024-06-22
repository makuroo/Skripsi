using Card;

namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private CardTemplate _cardPrefab;
        [SerializeField] private List<CardDefinition> currentCurrentDeck = new();
        
        public List<CardDefinition> CurrentDeck
        {
            get => currentCurrentDeck;
            set => currentCurrentDeck = value;
        }
        
        public static DeckManager Instance {get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}


