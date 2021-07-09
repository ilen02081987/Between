using Between.SpellRecognition;
using Between.InputTracking.Trackers;

namespace Between.Spells
{
    public abstract class SvmBasedSpell : BaseSpell
    {
        public abstract override float CoolDownTime { get; }
        protected abstract SpellFigure _figure { get; }

        protected override BaseInputTracker tracker => _tracker;
        private SvmTracker _tracker;

        public SvmBasedSpell() : base()
        {
            _tracker = new SvmTracker(_figure);
        }

        protected abstract override void OnCompleteSpell();
    }
}