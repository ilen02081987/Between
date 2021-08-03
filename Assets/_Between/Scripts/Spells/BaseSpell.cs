using Between.InputTracking;
using Between.InputTracking.Trackers;

namespace Between.Spells
{
    public abstract class BaseSpell
    {
        protected abstract BaseInputTracker tracker { get; }
        protected abstract float _manaCoefficient { get; }

        protected float _spellLenght => InputLenghtCalculator.LastLenght;
        protected float _manaAmount => _spellLenght * _manaCoefficient;
        protected bool _enoughMana => _manaAmount <= Player.Instance.Mana.Value;

        public void Init()
        {
            tracker.Init();

            tracker.CanCompleteDraw += OnCanCompleteDraw;
            tracker.CompleteDraw += OnCompleteSpell;
            tracker.DrawFailed += OnDrawFailed;
        }

        public void Dispose()
        {
            tracker.CanCompleteDraw -= OnCanCompleteDraw;
            tracker.CompleteDraw -= OnCompleteSpell;
            tracker.DrawFailed -= OnDrawFailed;
        }

        protected abstract void OnCompleteSpell();
        protected virtual void OnCanCompleteDraw() { }
        protected virtual void OnDrawFailed() { }

        protected void PerformOnCastSpell() => Player.Instance.Mana.Remove(_manaAmount);
    }
}