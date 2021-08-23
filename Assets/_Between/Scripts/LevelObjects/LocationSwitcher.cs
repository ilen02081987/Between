using Knife.Portal;
using System;
using UnityEngine;

namespace Between.LevelObjects
{
    public class LocationSwitcher : InteractableObject
    {
        [SerializeField] private PortalTransition _portalVfx;

        private void Start()
        {
            _portalVfx.OpenPortal();
        }

        protected override void PerformOnInteract() { }
    }
}