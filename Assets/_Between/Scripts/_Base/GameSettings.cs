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
        public float ProjectilesSpawnOffset;

        [Header("Заклинание щита")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldSpellCooldown;

        [Header("Заклинание метеоритного дождя")]
        public float MeteorRainTrackerMinLenght;
        public float MeteorRainTrackerMaxLenght;
        public float MeteorRainSpellCooldown;
        public float MeteorsLinesDelay;
        public int MeteorsLinesCount;
        public int MeteorsCount;

        [Header("Префаб щита")]
        public float ShieldHealth;
        public float ShieldLifeTime;

        [Header("SVM")]
        public float DecideBorder;
        public bool EnableProbabilitiesLog;

        public void CreateInstance()
        {
            Instance = this;    
        }
    }
}