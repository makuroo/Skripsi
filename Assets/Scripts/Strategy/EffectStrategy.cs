using UnityEngine;

namespace Strategy
{
    public abstract class EffectStrategy : ScriptableObject
    {
        [Tooltip("Option Name")]
        public string EffectName;
        public abstract void Activate();
    }
}
