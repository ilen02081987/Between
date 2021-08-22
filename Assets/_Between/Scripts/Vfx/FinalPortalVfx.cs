using UnityEngine;
using Between.LevelObjects;

namespace Between.Vfx
{
    public class FinalPortalVfx : MonoBehaviour
    {
        [SerializeField] private LocationSwitcher _locationSwitcher;
        [SerializeField] private GameObject _vfx;

        private void Start()
        {
            _locationSwitcher.OnEnter += EnableVfx;
        }

        private void EnableVfx()
        {
            _locationSwitcher.OnEnter -= EnableVfx;
            _vfx.SetActive(true);
        }
    }
}