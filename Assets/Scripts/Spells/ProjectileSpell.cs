using Between.SpellsEffects.Projectile;
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
        private PolygonalChainTracker _tracker = new PolygonalChainTracker(0).AddLine().AddLineAtAngle(45f);

        private ProjectileSpawner _projectileSpawner = new ProjectileSpawner();

        protected override void OnCompleteSpell()
        {
            var points = _tracker.Verticies;
            var spawnPoint = points[1];

            var firstDirectionPoint = (points[0] - spawnPoint).normalized + spawnPoint;
            var secondDirectionPoint = (points[2] - spawnPoint).normalized + spawnPoint;

            var direction = spawnPoint - Vector3.Lerp(firstDirectionPoint, secondDirectionPoint, .5f);

            _projectileSpawner.Spawn(ConvertVector(spawnPoint), ConvertVector(direction), 1f);
        }

        protected override void OnDrawFailed()
        {
            
        }

        private Vector3 ConvertVector(Vector3 input) => GameCamera.ScreenToWorldPoint(input);
    }
}