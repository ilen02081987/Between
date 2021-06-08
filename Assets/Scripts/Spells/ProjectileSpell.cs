using Between.SpellsEffects.Projectile;
using Between.Teams;
using Between.UserInput.Trackers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class ProjectileSpell : BaseSpell
    {
        public override float CoolDownTime => 1f;

        protected override BaseInputTracker tracker => _tracker;
        //private PolygonalChainTracker _tracker = new PolygonalChainTracker(0).AddLine().AddLineAtAngle(45f);
        private CurveTracker _tracker = new CurveTracker(0).SetLenght(500, 1000);

        private ProjectileSpawner _projectileSpawner;

        private float _projectileSpeed = 10f;
        private float _spellDamage = 1f;

        public ProjectileSpell(Team team)
        {
            _projectileSpawner = new ProjectileSpawner(team);
        }

        protected override void OnCompleteSpell()
        {
            var points = _tracker.DrawPoints;
            var startPoint = ConvertVector(points[0]);
            var directionPoint = ConvertVector(points[points.Count - 1]);

            _projectileSpawner.Spawn(startPoint, (directionPoint - startPoint).normalized, _projectileSpeed, _spellDamage);
        }

        private Vector3 ConvertVector(Vector3 input) => GameCamera.ScreenToWorldPoint(input);
    }
}