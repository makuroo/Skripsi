using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardTemplate : MonoBehaviour
    {
        [SerializeField] private CardDefinition _cardDefinition;
        private Image _cardImage;

        private void Awake()
        {
            _cardImage = GetComponent<Image>();
            _cardImage.sprite = _cardDefinition.CardSprite;
        }

        public void ActivateLeftStrategy()
        {
            if(_cardDefinition.LeftEffect)
                _cardDefinition.LeftEffect.Activate();
            else
                Debug.Log("Left Strategy is empty");
        }

        public void ActivateRightStrategy()
        {
            if(_cardDefinition.RightEffect)
                _cardDefinition.RightEffect.Activate();
            else
                Debug.Log("Right Strategy is empty");
        }
    }
}
