using Between.SpellsEffects.Shield;
using Between.UserInput.Trackers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        protected override BaseInputTracker tracker => _tracker;
        private CurveTracker _tracker = new CurveTracker();

        private ShieldSpawner _shieldSpawner = new ShieldSpawner();

        protected override void OnCanCompleteDraw() { }

        protected override void OnCompleteDraw() => SpawnShields();

        protected override void OnDrawFailed() { }

        private void SpawnShields() => _shieldSpawner.Spawn(_tracker.DrawPoints);
    }
}