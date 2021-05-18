using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Between.Spells.Shield
{
    public class ShieldSpell : BaseSpell
    {
        protected override string _painterName => "ShieldPainter";

        private ShieldSpawner _shieldSpawner;

        private readonly float _spellLenght = 1000f;
        private readonly int _coolDownMs = 1000;
        private readonly int _maxShieldsCount = 5;

        private float _currentSpellLenght;
        private Vector3 _startPoint;

        public ShieldSpell() : base(1)
        {
            _shieldSpawner = new ShieldSpawner();
        }

        protected override void StartDraw(Vector3 point)
        {
            if (CompareState(State.Draw))
                return;
            SetState(State.Draw);

            _currentSpellLenght = 0f;
            _startPoint = point;

            Debug.Log("Start draw shield");
        }

        protected override void Draw(Vector3 point)
        {
            if (!CompareState(State.Draw))
                return;

            _currentSpellLenght = Vector3.Distance(point, _startPoint);

            if (_currentSpellLenght > _spellLenght)
                FinishDraw(point);
        }

        protected override void PauseDraw(Vector3 point) { }

        protected override void FinishDraw(Vector3 point)
        {
            if (!CompareState(State.Draw))
                return;

            int shieldsCount = (int)(_currentSpellLenght / (_spellLenght / _maxShieldsCount));
            Vector3[] shieldsPoints = new Vector3[shieldsCount];

            for (int i = 0; i < shieldsCount; i++)
                shieldsPoints[i] = Vector3.Lerp(_startPoint, point, (float)i / shieldsCount);

            if (shieldsPoints.Length > 0)
                _shieldSpawner.SpawnShields(TestPlayer.Instance.transform, shieldsPoints);

            EnableCoolDown();

            Debug.Log("Finish draw shield");
        }

        private async void EnableCoolDown()
        {
            Debug.Log("Start CoolDown");
            SetState(State.Cooldown);

            await Task.Delay(_coolDownMs);

            Debug.Log("Finish CoolDown");
            SetState(State.None);
        }
    }
}