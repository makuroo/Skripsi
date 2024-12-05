using System;
using AYellowpaper.SerializedCollections;
using Manager;
using UnityEngine;

namespace Strategy
{
    [System.Serializable]
    public class ScoreEffect : EffectStrategy
    {
        public SerializedDictionary<ScoresEnum, int> EffectedScore = new();

        public override void Activate()
        {
            GameManager.Instance.UpdateScoreDictionary(EffectedScore);
        }

        public override void UpdateIndicatorImage(float alphaAmount)
        {
            foreach (var s in EffectedScore.Keys)
            {
                UIManager.UpdateScoreIndicatorAlpha?.Invoke(s,alphaAmount);
            }
        }

        public override void ResetIndicatorImage()
        {
            foreach (var s in EffectedScore.Keys)
            {
                GameManager.Instance.UIManager.ScoreIndicatorDictionary[s].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
