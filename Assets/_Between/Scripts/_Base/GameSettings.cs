using Between.Spells;
using UnityEngine;

namespace Between
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance { get; private set; }

        [Header("Заклинание прожектайла")]
        public float ProjectileMinLenght;
        public float ProjectileSpellCooldown;
        public float ProjectilesSpawnOffset;
        public ProjectileSpell.ProjectileDrawType ProjectileDrawType;

        [Header("Заклинание щита")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldSpellCooldown;

        [Header("Заклинание метеоритного дождя")]
        public float MeteorRainMinLenght;
        public float MeteorRainSpellCooldown;
        public float MeteorsLinesDelay;
        public int MeteorsLinesCount;
        public int MeteorsCount;

        [Header("SVM")]
        public float DecideBorder;
        public bool EnableProbabilitiesLog;

        [Header("Mavka")]
        public float RangeDetectionRadius;
        public float RangeCooldownBase;
        public float RangeCooldownShift;
        public float SpellPictureCompressCoefficient;
        public float SeveralProjectilesCastTime;
        public float SeveralProjectilesCastDelay;
        public bool SeveralProjectilesSingleCast;
        public float SeveralProjectilesSphereCastRadius;
        public float MavkaShieldsCooldownTime;
        public float MavkaSingleProjectileCastTime;
        public int SeveralProjectilesCastWeight;
        public int SingleProjectilesCastWeight;
        public float MeleeDetectionRadius;
        public float MeleeCooldownBase;
        public float MeleeCooldownShift;

        public void CreateInstance()
        {
            Instance = this;    
        }
    }
}