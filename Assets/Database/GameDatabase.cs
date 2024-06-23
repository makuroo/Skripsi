using System;
using System.Collections.Generic;
using Card;
using UnityEngine;

namespace Database
{
    public class GameDatabase : MonoBehaviour
    {
        [SerializeField] private List<CardDefinition> _cardPool;

        public List<CardDefinition> CardPool => _cardPool;

        public static GameDatabase Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}
