using Between.SpellsEffects.Projectile;
using UnityEngine;

namespace Between.SpellsEffects.MeteorRain
{
    public class MeteorRainSpawner
    {
        private ProjectileSpawner _projectileSpawner;
        private int _meteorsCount => GameSettings.Instance.MeteorsCount;

        public MeteorRainSpawner(string projectileName)
        {
            _projectileSpawner = new ProjectileSpawner(projectileName, 0f);
        }

        public void Spawn(Vector3 from, Vector3 to)
        {
            var meteorSize = _projectileSpawner.ElementSize;
            var distance = Vector3.Distance(from, to);
            var meteorsCount = Mathf.Min(distance / meteorSize, _meteorsCount);

            for (int i = 0; i < meteorsCount; i++)
                _projectileSpawner.Spawn(Vector3.Lerp(from, to, (float)i / meteorsCount), Vector3.down);
        }
    }
}