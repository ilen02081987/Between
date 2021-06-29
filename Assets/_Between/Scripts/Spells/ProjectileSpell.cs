using Between.Extensions;
using Between.SpellRecognition;
using Between.SpellsEffects.Projectile;
using Between.UserInput.Trackers;
using UnityEngine;

namespace Between.Spells
{
    public class ProjectileSpell : SvmBasedSpell
    {
        public override float CoolDownTime => GameSettings.Instance.ProjectileSpellCooldown;

        protected override SpellFigure _figure => SpellFigure.Line;

        private readonly ProjectileSpawner _projectileSpawner;
        private readonly float _spawnOffset = GameSettings.Instance.ProjectilesSpawnOffset;

        private bool _isLongEnough
        {
            get
            {
                var points = ((SvmTracker)tracker).DrawPoints;

                if (points.Count < 2)
                    return false;

                var distance = Vector2.Distance(points[0], points[points.Count - 1]);
                return distance > GameSettings.Instance.ProjectileMinLenght;
            }
        }

        public ProjectileSpell(string projectileName) : base()
        {
            _projectileSpawner = new ProjectileSpawner(projectileName, _spawnOffset);
        }

        protected override void OnCompleteSpell()
        {
            if (_isLongEnough)
                SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            var points = ((SvmTracker)tracker).DrawPoints;
            var startPoint = ConvertVector(points[0]);
            var directionPoint = ConvertVector(points[points.Count - 1]);

            if (GameSettings.Instance.ProjectileDrawType == ProjectileDrawType.Slingshot)
            {
                var tempPoint = startPoint;
                startPoint = directionPoint;
                directionPoint = tempPoint;
            }

            _projectileSpawner.Spawn(startPoint, (directionPoint - startPoint).normalized);
        }

        private Vector3 ConvertVector(Vector2Int input) 
            => GameCamera.ScreenToWorldPoint(input);

        public enum ProjectileDrawType
        {
            Spell = 0,
            Slingshot
        }
    }
}