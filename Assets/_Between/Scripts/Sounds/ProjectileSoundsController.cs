using UnityEngine;
using Between.SpellsEffects.Projectile;

namespace Between.Sounds
{
    public class ProjectileSoundsController : MonoBehaviour
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private AudioClip _launchClip;
        [SerializeField] private AudioClip _hitClip;

        private void Awake()
        {
            _projectile.OnLaunch += PlayLaunchSound;
            _projectile.OnHit += PlayHitSound;
        }

        private void OnDestroy()
        {
            _projectile.OnLaunch -= PlayLaunchSound;
            _projectile.OnHit -= PlayHitSound;
        }

        private void PlayLaunchSound(Vector3 obj) => PlayClip(_launchClip);
        private void PlayHitSound() => PlayClip(_hitClip);

        private void PlayClip(AudioClip clip)
        {
            if (clip == null)
                return;

            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}