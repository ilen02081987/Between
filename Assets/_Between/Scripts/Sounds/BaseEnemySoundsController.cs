using UnityEngine;
using Between.Enemies;

namespace Between.Sounds
{
    public class BaseEnemySoundsController : MonoBehaviour
    {
        [SerializeField] private BaseEnemy _enemy;
        [SerializeField] private AudioClip _death;

        private void Awake()
        {
            _enemy.OnDie += PlayDeathClip;
        }

        private void OnDestroy()
        {
            _enemy.OnDie -= PlayDeathClip;
        }

        private void PlayDeathClip()
        {
            AudioSource.PlayClipAtPoint(_death, transform.position);
        }
    }
}