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

        public ProjectileSpell(string projectileName) : base()
        {
            _projectileSpawner = new ProjectileSpawner(projectileName, _spawnOffset);
        }

        protected override void OnCompleteSpell()
        {
            var points = ((SvmTracker)tracker).DrawPoints;
            var startPoint = ConvertVector(points[0]);
            var directionPoint = ConvertVector(points[points.Count - 1]);

            _projectileSpawner.Spawn(startPoint, (directionPoint - startPoint).normalized);
        }

        private Vector3 ConvertVector(Vector2Int input) 
            => GameCamera.ScreenToWorldPoint(input.ToVector3());
    }
}