using UnityEngine;
using Between.LevelObjects;

namespace Between.Sounds
{
    public class ChestSoundsController : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private Chest _chest;

        private void Awake()
        {
            _chest.OnOpen += PlayClip;
        }

        private void OnDestroy()
        {
            _chest.OnOpen -= PlayClip;
        }

        private void PlayClip()
        {
            if (_clip != null)
                AudioSource.PlayClipAtPoint(_clip, transform.position);
        }
    }
}