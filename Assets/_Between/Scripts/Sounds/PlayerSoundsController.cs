using UnityEngine;
using Between.MainCharacter;

namespace Between.Sounds
{
    public class PlayerSoundsController : BaseSoundsController
    {
        [SerializeField] private LocomotionController _locomotionController;

        [Space]
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _step;
        [SerializeField] private AudioClip _land;

        private void Start()
        {
            _locomotionController.OnJump += PlayJumpClip;
            _locomotionController.OnLand += PlayLandClip;
        }

        private void OnDestroy()
        {
            _locomotionController.OnJump -= PlayJumpClip;
            _locomotionController.OnLand -= PlayLandClip;
        }

        private void PlayLandClip() => Play(_land);
        private void PlayJumpClip() => Play(_jump);

        //call from unity animator
        private void PerformOnStep() => Play(_step);
    }
}