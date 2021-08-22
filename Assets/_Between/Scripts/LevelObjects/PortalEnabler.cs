using UnityEngine;

namespace Between.LevelObjects
{
    public class PortalEnabler : LockableObject
    {
        [SerializeField] private LocationSwitcher _locationSwitcher;

        protected override void InteractAfterUnlock()
        {
            _locationSwitcher.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}