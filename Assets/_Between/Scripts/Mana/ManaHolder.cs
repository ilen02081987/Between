using System;
using System.Collections;
using UnityEngine;
using Between.Utilities;

namespace Between.Mana
{
    public class ManaHolder
    {
        public event Action OnValueChanged;

        public float Value { get; private set; }

        public readonly float MaxValue;

        private readonly float _recoverySpeed;

        public ManaHolder(float maxValue, float recoverySpeed)
        {
            Value = MaxValue = maxValue;
            _recoverySpeed = recoverySpeed;
        }

        public void StartRecover() => CoroutineLauncher.Start(Recover());
        public void StopRecover() => CoroutineLauncher.Stop(Recover());

        public void Remove(float removeValue)
        {
            LogRemove(removeValue);

            if (removeValue > Value)
                throw new Exception("Can't remove more mana then you have");

            Value -= removeValue;
            OnValueChanged?.Invoke();
        }

        public void Add(float addend)
        {
            Value = Mathf.Min(Value + addend, MaxValue);
            OnValueChanged?.Invoke();
        }

        private IEnumerator Recover()
        {
            while (Application.isPlaying)
            {
                if (Value < MaxValue)
                {
                    Value = Mathf.Min(Value + _recoverySpeed * Time.deltaTime, MaxValue);
                    OnValueChanged?.Invoke();
                }

                yield return null;
            }
        }

        private void LogRemove(float removeValue)
        {
            if (GameSettings.Instance.EnableRemoveManaLog)
                Debug.Log($"[ManaHolder] - Value = {Value}, try remove {removeValue}");
        }
    }
}