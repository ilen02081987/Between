using Between.SpellsEffects.ShieldSpell;
using Between.InputTracking.Trackers;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        protected override BaseInputTracker tracker => _tracker;
        protected override float _manaCoefficient => GameSettings.Instance.ShieldManaCoefficient;

        private ShieldCurveTracker _tracker = new ShieldCurveTracker(1);

        //private CurveTracker _tracker = new CurveTracker(1).
        //    SetForceEndAngle(GameSettings.Instance.ShieldTrackerForceEndAngle).
        //    SetLenght(GameSettings.Instance.ShieldTrackerMinLenght, GameSettings.Instance.ShieldTrackerMaxLenght);
        
        private ShieldSpawner _shieldSpawner = new ShieldSpawner("Shield");

        protected override void OnCompleteSpell()
        {
            if (_enoughManaForInputLenght)
            {
                SpawnShields();
                SpendDefaultMana();
            }
            else
            {
                InvokeNotEnoughManaEvent();
            }
        }

        private void SpawnShields()
        {
            _shieldSpawner.SpawnFromScreenPoints(_tracker.DrawPoints);
        }
    }
}