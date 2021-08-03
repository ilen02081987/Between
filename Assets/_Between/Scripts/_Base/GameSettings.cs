using System;
using UnityEngine;
using Between.Spells;

namespace Between
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance { get; private set; }

        [Header("Мана игрока")]
        public float ManaMaxValue;
        public float ManaRecoveryPerSec;
        public float ManaRecoveryDelay;
        public bool EnableRemoveManaLog;

        [Header("Бутылки с маной")]
        public float ManaBottleValue;

        [Header("Заклинание прожектайла")]
        public float ProjectilesSpawnOffset;
        public ProjectileSpell.ProjectileDrawType ProjectileDrawType;
        public float ProjectileMinLenght;
        public float ProjectileMaxLenght;
        public float ProjectileManaCoefficient;
        public float ProjectilePowerValue;
        public float ProjectileBaseDamageValue;
        public float ProjectileMinSize;
        public float ProjectileMaxSize;

        [Header("Заклинание щита")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldManaCoefficient;

        [Header("Заклинание метеоритного дождя")]
        public float MeteorRainMinLenght;
        public float MeteorsLinesDelay;
        public int MeteorsLinesCount;
        public int MeteorsCount;
        public float MeteorRainManaCoefficient;

        [Header("Заклинание лечения")]
        public float HealingSpellManaCoefficient;
        public float HealingSpellMaxSize;
        public float HealingSpellMaxHeal;
        public float CircleCompressionRatio;

        [Header("SVM")]
        public float[] DecideBorder;
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
        public int SingleProjectileCastWeight;
        public float MeleeDetectionRadius;
        public float MeleeCooldownBase;
        public float MeleeCooldownShift;

        [Header("EnemyUI")]
        public bool EnemyHealthUIEnabled;

        [Header("State machine")]
        public bool EnableStateMachineLogs;

        [Header("Lich")]
        public LichSettings Lich;

        public void CreateInstance()
        {
            Instance = this;
            InitSettings();
        } 

        public void ClearInstance()
        {
            Instance = null;
        }

        private void InitSettings()
        {
             if (DecideBorder == null || DecideBorder.Length < 4)
                DecideBorder = new float[4] { .9f, .9f, .9f, .9f };
        }

        [Serializable]
        public class LichSettings
        {
            public float RangeDetectionRadius;
            public float RangeCooldownBase;
            public float RangeCooldownShift;
            public float SpellPictureCompressCoefficient;
            public float SeveralProjectilesCastTime;
            public float SeveralProjectilesCastDelay;
            public bool SeveralProjectilesSingleCast;
            public float SeveralProjectilesSphereCastRadius;
            public float ShieldsCooldownTime;
            public float SingleProjectileCastTime;
            public int SeveralProjectilesCastWeight;
            public int SingleProjectileCastWeight;
            public float MeleeDetectionRadius;
            public float MeleeCooldownBase;
            public float MeleeCooldownShift;
        }
    }
}