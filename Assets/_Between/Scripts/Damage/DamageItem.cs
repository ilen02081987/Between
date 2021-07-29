using System;

namespace Between.Damage
{
    [Serializable]
    public class DamageItem
    {
        public readonly DamageType Type;
        public float Value;

        public DamageItem(DamageType damageType, float value)
        {
            Type = damageType;
            Value = value;
        }
    }
}