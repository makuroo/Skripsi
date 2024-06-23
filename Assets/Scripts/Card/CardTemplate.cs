using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardTemplate : MonoBehaviour
    {
        [SerializeField] private CardDefinition _cardDefinition;
        [SerializeField] private TMP_Text _leftOptionText;
        [SerializeField] private TMP_Text _rightOptionText;
        private Image _cardImage;

        public TMP_Text LeftOptionText
        {
            get => _leftOptionText;
            set => _leftOptionText = value;
        }

        public TMP_Text RightOptionText
        {
            get => _rightOptionText;
            set => _rightOptionText = value;
        }

        private void Awake()
        {
            _cardImage = GetComponent<Image>();
            _cardImage.sprite = _cardDefinition.CardSprite;
            LeftOptionText.text = _cardDefinition.LeftEffect.EffectName;
            RightOptionText.text = _cardDefinition.RightEffect.EffectName;
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
