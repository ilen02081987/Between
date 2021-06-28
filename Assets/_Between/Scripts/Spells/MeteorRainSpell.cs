using Between.Extensions;
using Between.SpellRecognition;
using Between.SpellsEffects.MeteorRain;
using Between.UserInput.Trackers;

namespace Between.Spells
{
    public class MeteorRainSpell : SvmBasedSpell
    {
        public override float CoolDownTime => GameSettings.Instance.MeteorRainSpellCooldown;

        protected override SpellFigure _figure => SpellFigure.CheckMark;

        private MeteorRainSpawner _spawner;

        public MeteorRainSpell(string projectileName) : base()
        {
            _spawner = new MeteorRainSpawner(projectileName);
        }

        protected override void OnCompleteSpell()
        {
            var drawPoints = ((SvmTracker)tracker).DrawPoints;

            var upperPoint = drawPoints.FindUpperPoint();
            var leftPoint = drawPoints.FindLeftPoint();
            var rightPoint = drawPoints.FindRightPoint();

            leftPoint.y = rightPoint.y = upperPoint.y;

            _spawner.Spawn(
                GameCamera.ScreenToWorldPoint(leftPoint.ToVector3()),
                GameCamera.ScreenToWorldPoint(rightPoint.ToVector3()));
        }
    }
}