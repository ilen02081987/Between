using Between.Spells;
using UnityEngine;

namespace Between
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Instance { get; private set; }

        [Header("���������� �����������")]
        public float ProjectileTrackerMinLenght;
        public float ProjectileTrackerMaxLenght;
        public float ProjectileTrackerForceEndAngle;
        public float ProjectileSpellCooldown;
        public float ProjectilesSpawnOffset;
        public ProjectileSpell.ProjectileDrawType ProjectileDrawType;

        [Header("���������� ����")]
        public float ShieldTrackerMinLenght;
        public float ShieldTrackerMaxLenght;
        public float ShieldTrackerForceEndAngle;
        public float ShieldSpellCooldown;

        [Header("���������� ������������ �����")]
        public float MeteorRainTrackerMinLenght;
        public float MeteorRainTrackerMaxLenght;
        public float MeteorRainSpellCooldown;
        public float MeteorsLinesDelay;
        public int MeteorsLinesCount;
        public int MeteorsCount;

        [Header("������ ����")]
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