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
            _locationSwitcher.OnInteract += EnableVfx;
        }

        private void EnableVfx()
        {
            _locationSwitcher.OnInteract -= EnableVfx;
            _vfx.SetActive(true);
        }
    }
}