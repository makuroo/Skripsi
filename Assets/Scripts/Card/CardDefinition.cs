using Strategy;

namespace Card
{
    using UnityEngine;
    
    public enum CardType
    {
        Alur,
        Good,
        Bad,
        Special,
        Information
    }
    [CreateAssetMenu(menuName = "SciptableObjects/Card")]
    public class CardDefinition : ScriptableObject
    {
        public CardType Type;
        public string CardName;
        public string CardDescription;
        public Sprite CardSprite;
        public EffectStrategy LeftEffect;
        public EffectStrategy RightEffect;
    }
}

