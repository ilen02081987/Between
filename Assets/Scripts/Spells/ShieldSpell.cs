using Between.SpellsEffects.Shield;
using Between.UserInput.Trackers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        public override float CoolDownTime => 1f;

        protected override BaseInputTracker tracker => _tracker;
        private CurveTracker _tracker = new CurveTracker(1).SetForceEndAngle(90f);

        private ShieldSpawner _shieldSpawner = new ShieldSpawner();

        protected override void OnCompleteSpell() => SpawnShields();

        private void SpawnShields() => _shieldSpawner.Spawn(_tracker.DrawPoints);
    }
}