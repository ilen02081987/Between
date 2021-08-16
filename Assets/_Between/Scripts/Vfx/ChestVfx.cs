using UnityEngine;
using Between.LevelObjects;

namespace Between.Vfx
{
    public class ChestVfx : MonoBehaviour
    {
        [SerializeField] private Chest _chest;
        [SerializeField] private GameObject _vfx;

        private void Start()
        {
            _chest.OnOpen += EnableVfx;
        }

        private void EnableVfx()
        {
            _vfx.SetActive(true);
        }
    }
}