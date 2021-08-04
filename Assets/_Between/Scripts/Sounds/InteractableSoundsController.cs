using UnityEngine;
using Between.LevelObjects;

namespace Between.Sounds
{
    public class InteractableSoundsController : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private InteractableObject _interactableObject;

        private void Awake()
        {
            _interactableObject.OnInteract += PlayClip;
        }

        private void OnDestroy()
        {
            _interactableObject.OnInteract -= PlayClip;
        }

        private void PlayClip()
        {
            if (_clip != null)
                AudioSource.PlayClipAtPoint(_clip, transform.position);
        }
    }
}