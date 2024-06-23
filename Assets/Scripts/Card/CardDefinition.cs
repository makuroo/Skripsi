using System;
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
    [CreateAssetMenu(menuName = "ScriptableObjects/Card")]
    public class CardDefinition : ScriptableObject, IEquatable<CardDefinition>
    {
        public CardType Type;
        public string CardName;
        [TextArea]
        public string CardDescription;
        public Sprite CardSprite;
        public EffectStrategy LeftEffect;
        public EffectStrategy RightEffect;

        public bool Equals(CardDefinition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) &&
                   Type == other.Type &&
                   CardName == other.CardName &&
                   CardDescription == other.CardDescription &&
                   Equals(CardSprite,
                       other.CardSprite) &&
                   Equals(LeftEffect,
                       other.LeftEffect) &&
                   Equals(RightEffect,
                       other.RightEffect);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CardDefinition)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), (int)Type, CardName, CardDescription, CardSprite, LeftEffect, RightEffect);
        }
    }
}

