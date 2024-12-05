using Manager;
using UnityEngine;

namespace Strategy
{
    [System.Serializable]
    public class InformationEffect : EffectStrategy
    {
        [SerializeField] 
        private GameObject _informationPanel;

        public override void Activate()
        {
           Object.Instantiate(_informationPanel,Vector3.zero, Quaternion.identity);
        }

        public override void UpdateIndicatorImage(float alphaAmount)
        {
            throw new System.NotImplementedException();
        }

        public override void ResetIndicatorImage()
        {
            throw new System.NotImplementedException();
        }
    }
}
