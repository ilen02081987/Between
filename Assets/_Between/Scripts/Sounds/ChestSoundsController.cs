using UnityEngine;
using Between.LevelObjects;

namespace Between.Sounds
{
    public class ChestSoundsController : BaseSoundsController
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private Chest _chest;

        private void Start() => _chest.OnOpen += PlayClip;
        private void OnDestroy() => _chest.OnOpen -= PlayClip;
        private void PlayClip() => Play(_clip);
    }
}