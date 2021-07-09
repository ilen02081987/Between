using Between.SpellsEffects.ShieldSpell;
using Between.InputTracking.Trackers;

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
        
        private ShieldSpawner _shieldSpawner = new ShieldSpawner("Shield", Player.Instance.Controller.transform);

        private GameSettings _settings => GameSettings.Instance;

        protected override void OnCompleteSpell()
        {
            if (_enoughMana)
            {
                SpawnShields();
                RemoveMana();
            }
        }

        private void SpawnShields() => _shieldSpawner.SpawnFromScreenPoints(_tracker.DrawPoints);
    }
}