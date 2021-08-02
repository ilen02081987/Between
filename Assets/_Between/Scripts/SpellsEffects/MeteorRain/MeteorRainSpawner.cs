using System.Collections;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Utilities;

namespace Between.SpellsEffects.MeteorRain
{
    public class MeteorRainSpawner
    {
        private ProjectileSpawner _projectileSpawner;
        private int _meteorsCount => GameSettings.Instance.MeteorsCount;
        private int _linesCount => GameSettings.Instance.MeteorsLinesCount;
        private float _linesDelay => GameSettings.Instance.MeteorsLinesDelay;

        public MeteorRainSpawner(string projectileName)
        {
            _projectileSpawner = new ProjectileSpawner(projectileName);
        }

        public void Spawn(Vector3 from, Vector3 to)
        {
            float meteorSize = _projectileSpawner.ElementSize;
            float distance = Vector3.Distance(from, to);
            int meteorsCount = (int)Mathf.Min(distance / meteorSize, _meteorsCount);

            CoroutineLauncher.Start(PerformSpawn(from, to, meteorsCount));
        }

        private IEnumerator PerformSpawn(Vector3 from, Vector3 to, int count)
        {
            for (int i = 0; i < _linesCount; i++)
            {
                SpawnLine(from, to, count);
                yield return new WaitForSeconds(_linesDelay);
            }
        }

        private void SpawnLine(Vector3 from, Vector3 to, int count)
        {
            for (int i = 0; i < count; i++)
                _projectileSpawner.Spawn(Vector3.Lerp(from, to, (float)i / count), Vector3.down);
        }
    }
}