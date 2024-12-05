using System.Collections.Generic;
using Strategy;
using UnityEngine;

namespace Card
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CardEffect")]
    public class CardEffect : ScriptableObject
    {
        public string EffectName;
        
        [SerializeReference,SubclassSelector] 
        private List<EffectStrategy> _effectStrategies=new();

        public void Activate()
        {
            foreach (var e in _effectStrategies)
            {
                e?.Activate();
            }
        }

        public void UpdateIndicator(float alpha)
        {
            foreach (var e in _effectStrategies)
            {
                e?.UpdateIndicatorImage(alpha);
            }
        }

        public void ResetIndicator()
        {
            foreach (var e in _effectStrategies)
            {
                e?.ResetIndicatorImage();
            }
        }
    }
}