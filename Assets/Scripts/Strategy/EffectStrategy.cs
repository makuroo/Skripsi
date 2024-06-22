using UnityEngine;

namespace Strategy
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void Activate();
    }
}
