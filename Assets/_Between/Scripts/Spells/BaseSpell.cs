using Between.InputTracking;
using Between.InputTracking.Trackers;
using System;

namespace Between.Spells
{
    public abstract class BaseSpell
    {
        public static event Action SpellCasted;
        public static event Action NotEnoughMana;
        public static event Action NotRecognizeSpell;

        protected abstract BaseInputTracker tracker { get; }
        protected abstract float _manaCoefficient { get; }

        protected bool _enoughManaForInputLenght => EnoughMana(_spellLenght);
        protected float _spellLenght => InputLenghtCalculator.LastLenght;

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

        protected void SpendDefaultMana() => SpendManaForSpell(_spellLenght);
        protected void SpendManaForSpell(float spellLenght)
        {
            Player.Instance.Mana.Remove(CalculateMana(spellLenght));
            SpellCasted?.Invoke();
        }
        protected bool EnoughMana(float spellLenght) => CalculateMana(spellLenght) <= Player.Instance.Mana.Value;
        protected float CalculateMana(float spellLenght) => spellLenght * _manaCoefficient;

        protected void InvokeNotEnoughManaEvent() => NotEnoughMana?.Invoke();
        protected void InvokeNotRecognizeEvent() => NotRecognizeSpell?.Invoke();
    }
}