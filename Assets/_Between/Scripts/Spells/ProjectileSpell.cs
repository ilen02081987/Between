using UnityEngine;
using Between.SpellRecognition;
using Between.SpellsEffects.Projectile;
using Between.UserInput.Trackers;

namespace Between.Spells
{
    public class ProjectileSpell : SvmBasedSpell
    {
        public override float CoolDownTime => _coolDownTime;

        protected override SpellFigure _figure => SpellFigure.Line;

        private readonly ProjectileSpawner _projectileSpawner;
        private readonly float _spawnOffset = GameSettings.Instance.ProjectilesSpawnOffset;
        private readonly float _minLenght;
        private readonly float _maxLenght;
        private readonly float _coolDownTime; 

        private bool _isValidLenght
        {
            get
            {
                var points = ((SvmTracker)tracker).DrawPoints;

                if (points.Count < 2)
                    return false;

                var distance = Vector2.Distance(points[0], points[points.Count - 1]);
                return distance > _minLenght && distance < _maxLenght;
            }
        }

        public ProjectileSpell(string projectileName, float cooldown, float minLenght, float maxLenght, float spawnDelay = 0f) : base()
        {
            _coolDownTime = cooldown;
            _projectileSpawner = new ProjectileSpawner(projectileName, _spawnOffset, spawnDelay);
            
            _minLenght = minLenght;
            _maxLenght = maxLenght;
        }

        protected override void OnCompleteSpell()
        {
            if (_isValidLenght)
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