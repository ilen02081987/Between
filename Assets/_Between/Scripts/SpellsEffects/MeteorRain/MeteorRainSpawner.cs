using System.Collections;
using System.IO;
using UnityEngine;
using Between.SpellsEffects.Projectile;
using Between.Sounds;
using RH.Utilities.Coroutines;

namespace Between.SpellsEffects.MeteorRain
{
    public class MeteorRainSpawner
    {
        private ProjectileSpawner _projectileSpawner;
        private int _meteorsCount => GameSettings.Instance.MeteorsCount;
        private int _linesCount => GameSettings.Instance.MeteorsLinesCount;
        private float _linesDelay => GameSettings.Instance.MeteorsLinesDelay;

        private static AudioClip _spawnSound;

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

            PlaySpawnSound(Vector3.Lerp(from, to, .5f));
        }

        private void PlaySpawnSound(Vector3 position)
        {
            if (_spawnSound == null)
                _spawnSound = Resources.Load<AudioClip>(Path.Combine(ResourcesFoldersNames.SOUNDS, "meteor_rain"));

            AudioSource.PlayClipAtPoint(_spawnSound, position, Volume.Value);
        }
    }
}