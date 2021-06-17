using Between.SpellsEffects.ShieldSpell;
using Between.UserInput.Trackers;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        public override float CoolDownTime => GameSettings.Instance.ShieldSpellCooldown;

        protected override BaseInputTracker tracker => _tracker;
        
        private CurveTracker _tracker = new CurveTracker(1).
            SetForceEndAngle(GameSettings.Instance.ShieldTrackerForceEndAngle).
            SetLenght(GameSettings.Instance.ShieldTrackerMinLenght, GameSettings.Instance.ShieldTrackerMaxLenght);
        
        private ShieldSpawner _shieldSpawner = new ShieldSpawner();

        protected override void OnCompleteSpell() => SpawnShields();

        private void SpawnShields() => _shieldSpawner.Spawn(_tracker.DrawPoints);
    }
}