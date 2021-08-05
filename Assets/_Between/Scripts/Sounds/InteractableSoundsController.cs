using UnityEngine;
using Between.LevelObjects;

namespace Between.Sounds
{
    public class InteractableSoundsController : BaseSoundsController
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private InteractableObject _interactableObject;

        private void Start()
        {
            _interactableObject.OnInteract += PlayClip;
        }

        private void OnDestroy()
        {
            _interactableObject.OnInteract -= PlayClip;
        }

        private void PlayClip()
        {
            Play(_clip);
        }
    }
}