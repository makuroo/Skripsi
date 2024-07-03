using Card;
using Manager;
using UnityEngine;

namespace Strategy
{
    [System.Serializable]
    public class DeckEffect  : EffectStrategy
    {
        [SerializeField] private CardDefinition _cardToAdd;
        [SerializeField] private int _targetIndex;
        [SerializeField] private State _targetState;
        public override void Activate()
        {
            DeckManager.Instance.InsertCardToDeck(_targetState,_targetIndex,_cardToAdd);
        }
    }
}
