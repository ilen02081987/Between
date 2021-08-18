using Between.Extensions;
using Between.SpellRecognition;
using Between.SpellsEffects.MeteorRain;
using Between.InputTracking.Trackers;
using Between.InputTracking;

namespace Between.Spells
{
    public class MeteorRainSpell : SvmBasedSpell
    {
        protected override SpellFigure _figure => SpellFigure.CheckMark;

        private MeteorRainSpawner _spawner;

        private bool _isLongEnough => InputLenghtCalculator.LastLenght > GameSettings.Instance.MeteorRainMinLenght;

        protected override float _manaCoefficient => GameSettings.Instance.MeteorRainManaCoefficient;

        public MeteorRainSpell(string projectileName) : base()
        {
            _spawner = new MeteorRainSpawner(projectileName);
        }

        protected override void OnCompleteSpell()
        {
            if (!_isLongEnough)
            {
                InvokeNotRecognizeEvent();
                return;
            }

            if (!_enoughManaForInputLenght)
            {
                InvokeNotEnoughManaEvent();
                return;
            }

            SpawnMeteorRain();
            SpendDefaultMana();
        }

        private void SpawnMeteorRain()
        {
            var drawPoints = ((SvmTracker)tracker).DrawPoints;

            var upperPoint = drawPoints.FindUpperPoint();
            var leftPoint = drawPoints.FindLeftPoint();
            var rightPoint = drawPoints.FindRightPoint();

            leftPoint.y = rightPoint.y = upperPoint.y;

            _spawner.Spawn(
                GameCamera.ScreenToWorldPoint(leftPoint),
                GameCamera.ScreenToWorldPoint(rightPoint));
        }
    }
}