using UnityEngine;

namespace Between
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance { get; private set; }

        [Header("Заклинание прожектайла")]
        public float ProjectileTrackerMinLenght;
        public float ProjectileTrackerMaxLenght;
        public float ProjectileTrackerForceEndAngle;
        public float ProjectileSpellCooldown;

        [Header("Заклинание щита")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldSpellCooldown;

        [Header("Префаб щита")]
        public float ShieldHealth;
        public float ShieldLifeTime;

        public void CreateInstance()
        {
            Instance = this;    
        }
    }
}