using Between.SpellsEffects.ShieldSpell;
using Between.InputTracking.Trackers;
using UnityEngine;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        public override float CoolDownTime => _settings.ShieldSpellCooldown;

        protected override BaseInputTracker tracker => _tracker;

        protected override float _manaCoefficient => _settings.ShieldManaCoefficient;

        private CurveTracker _tracker = new CurveTracker(1).
            SetForceEndAngle(GameSettings.Instance.ShieldTrackerForceEndAngle).
            SetLenght(GameSettings.Instance.ShieldTrackerMinLenght, GameSettings.Instance.ShieldTrackerMaxLenght);
        
        private ShieldSpawner _shieldSpawner = new ShieldSpawner("Shield", Player.Instance.Controller.transform, true);
        private ShieldSpawner _bridgeshieldSpawner = new ShieldSpawner("BridgeShield", Player.Instance.Controller.transform, false);

        private GameSettings _settings => GameSettings.Instance;

        protected override void OnCompleteSpell()
        {
            if (_enoughMana)
            {
                SpawnShields();
                PerformOnCastSpell();
            }
        }

        private void SpawnShields()
        {
            if (CanCreateBridge())
                _bridgeshieldSpawner.SpawnFromScreenPoints(_tracker.DrawPoints);
            else
                _shieldSpawner.SpawnFromScreenPoints(_tracker.DrawPoints);
        }

        private bool CanCreateBridge()
        {
            var checkPoint = GameCamera.ScreenToWorldPoint(_tracker.DrawPoints[0]);
            var colliders = Physics.OverlapSphere(checkPoint, 1f);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<ShieldBridgeZone>(out var zone))
                    return true;
            }

            return false;
        }
    }
}