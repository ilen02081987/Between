using Between.InputTracking;
using Between.InputTracking.Trackers;
using Between.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Between.Spells
{
    public abstract class BaseSpell
    {
        public event Action CooldownStarted;
        public event Action<float> CooldownUpdated;
        public event Action CooldownFinished;

        public abstract float CoolDownTime { get; }
        protected abstract BaseInputTracker tracker { get; }
        protected abstract float _manaCoefficient { get; }

        protected float _spellLenght => InputLenghtCalculator.LastLenght;
        protected float _manaAmount => _spellLenght * _manaCoefficient;
        protected bool _enoughMana => _manaAmount <= Player.Instance.Mana.Value;

        public void Init()
        {
            tracker.Init();

            tracker.CanCompleteDraw += OnCanCompleteDraw;
            tracker.CompleteDraw += OnCompleteDraw;
            tracker.DrawFailed += OnDrawFailed;
        }

        public void Dispose()
        {
            tracker.CanCompleteDraw -= OnCanCompleteDraw;
            tracker.CompleteDraw -= OnCompleteDraw;
            tracker.DrawFailed -= OnDrawFailed;
        }

        protected abstract void OnCompleteSpell();
        protected virtual void OnCanCompleteDraw() { }
        protected virtual void OnDrawFailed() { }

        protected void RemoveMana() => Player.Instance.Mana.Remove(_manaAmount);

        //NOTE: корутина вместо асинка чтобы ждать время игры, а не реалтайм
        private IEnumerator WaitCooldown()
        {
            CooldownStarted?.Invoke();

            tracker.Dispose();
            float currentTime = 0f;

            while (currentTime <= CoolDownTime)
            {
                CooldownUpdated?.Invoke(currentTime);

                currentTime += Time.deltaTime;
                yield return null;
            }

            tracker.Init();

            CooldownFinished?.Invoke();
        }

        private void OnCompleteDraw()
        {
            OnCompleteSpell();
            CoroutineLauncher.Start(WaitCooldown());
        }
    }
}