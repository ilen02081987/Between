using Between.Extensions;
using Between.SpellsEffects.MeteorRain;
using Between.UserInput.Trackers;

namespace Between.Spells
{
    public class MeteorRainSpell : BaseSpell
    {
        public override float CoolDownTime => GameSettings.Instance.MeteorRainSpellCooldown;

        protected override BaseInputTracker tracker => _tracker;

        private CheckMarkTracker _tracker = new CheckMarkTracker(0).
            SetLenght(GameSettings.Instance.MeteorRainTrackerMinLenght, GameSettings.Instance.MeteorRainTrackerMaxLenght);

        private MeteorRainSpawner _spawner;

        public MeteorRainSpell(string projectileName)
        {
            _spawner = new MeteorRainSpawner(projectileName);
        }

        protected override void OnCompleteSpell()
        {
            var drawPoints = _tracker.DrawPoints;

            var upperPoint = drawPoints.FindUpperPoint();
            var leftPoint = drawPoints.FindLeftPoint();
            var rightPoint = drawPoints.FindRightPoint();

            leftPoint.y = rightPoint.y = upperPoint.y;

            _spawner.Spawn(GameCamera.ScreenToWorldPoint(leftPoint), GameCamera.ScreenToWorldPoint(rightPoint));
        }
    }
}