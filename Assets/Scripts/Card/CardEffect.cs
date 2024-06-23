using System.Collections.Generic;
using Strategy;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CardEffect")]
    public class CardEffect : ScriptableObject
    {
        public string EffectName;
        [SerializeField] private List<EffectStrategy> _effectStrategies = new();

        public void Activate()
        {
            foreach (var e in _effectStrategies)
            {
                e.Activate();
            }
        }
    }
}