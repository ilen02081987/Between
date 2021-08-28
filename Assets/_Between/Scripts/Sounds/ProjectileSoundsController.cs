using UnityEngine;
using Between.SpellsEffects.Projectile;

namespace Between.Sounds
{
    public class ProjectileSoundsController : BaseSoundsController
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private AudioClip _launchClip;
        [Space]
        [SerializeField] private AudioClip _hitClip;
        [SerializeField, Range(0, 1)] private float _hitClipVolume = 1f;

        private void Start()
        {
            _projectile.OnLaunch += PlayLaunchSound;
            _projectile.OnHit += PlayHitSound;
        }

        private void OnDestroy()
        {
            _projectile.OnLaunch -= PlayLaunchSound;
            _projectile.OnHit -= PlayHitSound;
        }

        private void PlayLaunchSound(Vector3 obj) => Play(_launchClip);
        private void PlayHitSound(Collider collider) => PlayWithoutSource(_hitClip, _hitClipVolume);
    }
}