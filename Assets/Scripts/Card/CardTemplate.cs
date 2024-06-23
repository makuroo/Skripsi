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

        public CardDefinition Definition
        {
            get => _cardDefinition;
            set => _cardDefinition = value;
        }

        public void ActivateLeftStrategy()
        {
            if(Definition.LeftEffect)
                Definition.LeftEffect.Activate();
            else
                Debug.Log("Left Strategy is empty");
        }

        public void ActivateRightStrategy()
        {
            if(Definition.RightEffect)
                Definition.RightEffect.Activate();
            else
                Debug.Log("Right Strategy is empty");
        }

        public void Initialize()
        {
            _cardImage = GetComponent<Image>();
            _cardImage.sprite = Definition.CardSprite;
            LeftOptionText.text = Definition.LeftEffect.EffectName;
            //RightOptionText.text = Definition.RightEffect.EffectName;
        }
    }
}
