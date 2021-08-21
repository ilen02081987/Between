using UnityEngine;
using Between.LevelObjects;

namespace Between.Vfx
{
    public class ChestVfx : MonoBehaviour
    {
        [SerializeField] private Chest _chest;
        [SerializeField] private GameObject _vfx;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _chest.OnOpen += EnableVfx;
        }

        private void EnableVfx()
        {
            _vfx.SetActive(true);
            _animator.enabled = true;
        }
    }
}