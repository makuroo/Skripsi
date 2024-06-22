using Card;

namespace Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private CardTemplate _cardPrefab;
        [SerializeField] private List<CardDefinition> DeckCards = new();
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}


