using AYellowpaper.SerializedCollections;
using Manager;
using UnityEngine;

namespace Strategy
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Strategy/ScoreEffect")]
    public class ScoreEffect : EffectStrategy
    {
        public SerializedDictionary<ScoresEnum, int> EffectedScore = new();

        public override void Activate()
        {
            GameManager.Instance.UpdateScoreDictionary(EffectedScore);
        }
    }
}
