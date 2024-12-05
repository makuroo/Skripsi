using UnityEngine;
using UnityEngine.UI;

namespace Strategy
{
    [System.Serializable]
    public abstract class EffectStrategy
    {
        [SerializeField] protected Image _indicatorImage;
        public abstract void Activate();
        public abstract void UpdateIndicatorImage(float alphaAmount);
        public abstract void ResetIndicatorImage();
    }
}
