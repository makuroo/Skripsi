using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Card;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace Database
{
    public class GameDatabase : MonoBehaviour
    {
        [FormerlySerializedAs("_cardPool")] 
        [SerializeField] private SerializedDictionary<State, List<CardDefinition>> _cardPoolDictionary;

        public SerializedDictionary<State, List<CardDefinition>>  CardPoolDictionary => _cardPoolDictionary;

        public static GameDatabase Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
