using Card;
using Manager;
using UnityEngine;

namespace Strategy
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Strategy/DeckEffect")]
    public class DeckEffect  : EffectStrategy
    {
        [SerializeField] private CardDefinition _cardToAdd;
        [SerializeField] private int _targetIndex;
        public override void Activate()
        {
            DeckManager.Instance.CurrentDeck.Insert(_targetIndex, _cardToAdd);
        }
    }
}
