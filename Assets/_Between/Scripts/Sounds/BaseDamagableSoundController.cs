using UnityEngine;
using Between.Enemies;

namespace Between.Sounds
{
    public class BaseDamagableSoundController : BaseSoundsController
    {
        [SerializeField] private BaseDamagableObject _enemy;
        [SerializeField] private AudioClip _death;

        private void Start()
        {
            _enemy.OnDie += PlayDeathClip;
        }

        private void OnDestroy()
        {
            _enemy.OnDie -= PlayDeathClip;
        }

        private void PlayDeathClip() => Play(_death);
    }
}