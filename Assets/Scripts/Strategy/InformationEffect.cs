using UnityEngine;

namespace Strategy
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Strategy/InformationEffect")]
    public class InformationEffect : EffectStrategy
    {
        [SerializeField] private GameObject _informationPanel;

        public override void Activate()
        {
            _informationPanel.SetActive(true);
        }
    }
}
