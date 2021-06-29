using Between.UserInput.Trackers;
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

        private void OnCompleteDraw()
        {
            OnCompleteSpell();
            CoroutineLauncher.Start(WaitCooldown());
        }

        protected abstract void OnCompleteSpell();
        protected virtual void OnCanCompleteDraw() { }
        protected virtual void OnDrawFailed() { }

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
    }
}