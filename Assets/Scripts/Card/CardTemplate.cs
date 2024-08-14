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
        
        [SerializeField] private Image _cardImage;
        [SerializeField] private Image _cardBackgroundImage;

        public TMP_Text LeftOptionText => _leftOptionText;

        public TMP_Text RightOptionText => _rightOptionText;

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
                Debug.LogError("Left Strategy is empty");
        }

        public void ActivateRightStrategy()
        {
            if(Definition.RightEffect)
                Definition.RightEffect.Activate();
            else
                Debug.LogError("Right Strategy is empty");
        }

        public void Initialize()
        {
            if (_cardDefinition.CardSprite == null)
            {
                Debug.LogError(_cardDefinition+" "+"card sprite is null");
            }
            _cardImage.sprite = Definition.CardSprite;
            
            if (_cardDefinition.CardBackgroundSprite == null)
            {
                Debug.LogError(_cardDefinition+" "+"card background sprite is null");
            }
            _cardBackgroundImage.sprite = Definition.CardBackgroundSprite;
            _cardBackgroundImage.color = Definition.CardBackgroundColor;
            
            if (_cardDefinition.LeftEffect == null)
            {
                Debug.LogError(_cardDefinition+" "+"left effect is null");
            }
            LeftOptionText.text = Definition.LeftEffect.EffectName;
            
            if (_cardDefinition.CardSprite == null)
            {
                Debug.LogError(_cardDefinition+" "+"right effect is null");
            }
            RightOptionText.text = Definition.RightEffect.EffectName;
        }
    }
}
