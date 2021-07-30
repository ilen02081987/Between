using Between.Spells;
using System;
using UnityEngine;

namespace Between
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance { get; private set; }

        [Header("Мана игрока")]
        public float ManaMaxValue;
        public float ManaRecoveryPerSec;
        public bool EnableRemoveManaLog;

        [Header("Бутылки с маной")]
        public float ManaBottleValue;

        [Header("Заклинание прожектайла")]
        public float ProjectileMiddleLenght;
        public float ProjectilesSpawnOffset;
        public ProjectileSpell.ProjectileDrawType ProjectileDrawType;

        [Header("Заклинание малого прожектайла")]
        public float ProjectileMinLenght;
        public float ProjectileSpellCooldown;
        public float SmallProjectileManaCoefficient;

        [Header("Заклинание большого прожектайла")]
        public float ProjectileMaxLenght;
        public float BigProjectileSpellCooldown;
        public float BigProjectileCastDelay;
        public float BigProjectileManaCoefficient;

        [Header("Заклинание щита")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldSpellCooldown;
        public float ShieldManaCoefficient;

        [Header("Заклинание метеоритного дождя")]
        public float MeteorRainMinLenght;
        public float MeteorRainSpellCooldown;
        public float MeteorsLinesDelay;
        public int MeteorsLinesCount;
        public int MeteorsCount;
        public float MeteorRainManaCoefficient;

        [Header("Заклинание лечения")]
        public float HealingSpellCooldown;
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
        public int SingleProjectilesCastWeight;
        public float MeleeDetectionRadius;
        public float MeleeCooldownBase;
        public float MeleeCooldownShift;

        [Header("EnemyUI")]
        public bool EnemyHealthUIEnabled;

        [Header("State machine")]
        public bool EnableStateMachineLogs;

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
    }
}