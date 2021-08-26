using System;
using System.Collections.Generic;
using UnityEngine;
using Between.Enemies;

namespace Between.LevelObjects
{
    public class FightBorders : MonoBehaviour
    {
        public event Action OnActivate;

        [SerializeField] private List<BaseEnemy> _enemies;
        [SerializeField] private FightBorder _leftBorder;
        [SerializeField] private FightBorder _rightBorder;

        private float _playerPositionX => Player.Instance.Controller.Position.x;
        private bool _isEnabled = false;

        private bool _playerInBorders => _playerPositionX > _leftBorder.transform.position.x
            && _playerPositionX < _rightBorder.transform.position.x;

        public void AddEnemy(BaseEnemy enemy)
        {
            _enemies.Add(enemy);

            if (_isEnabled)
                enemy.OnDie += () => TryDestroyBorders(enemy);
        }

        private void Start() => AttachListeners();

        private void AttachListeners()
        {
            _leftBorder.OnPlayerExitCollider += TryEnableBorders;
            _rightBorder.OnPlayerExitCollider += TryEnableBorders;
        }

        private void TryEnableBorders()
        {
            if (_playerInBorders)
                EnableBorders();
        }

        private void EnableBorders()
        {
            DetachListeners();

            if (!NeedEnable())
                Destroy();

            if (_isEnabled)
                return;

            _leftBorder.Enable();
            _rightBorder.Enable();

            foreach (var enemy in _enemies)
                enemy.OnDie += () => TryDestroyBorders(enemy);

            _isEnabled = true;
            OnActivate?.Invoke();
        }

        private void DetachListeners()
        {
            _leftBorder.OnPlayerExitCollider += TryEnableBorders;
            _rightBorder.OnPlayerExitCollider += TryEnableBorders;
        }

        private bool NeedEnable()
        {
            CheckAliveEnemies();

            if (_enemies.Count == 0)
                return false;

            return true;
        }

        private void CheckAliveEnemies()
        {
            List<BaseEnemy> clearEnemies = new List<BaseEnemy>();

            foreach (var enemy in _enemies)
            {
                if (enemy != null)
                    clearEnemies.Add(enemy);
            }

            _enemies = clearEnemies;
        }

        private void TryDestroyBorders(BaseEnemy enemy)
        {
            _enemies.Remove(enemy);

            if (_enemies.Count == 0)
                Destroy();
        }

        private void Destroy()
        {
            if (this == null)
                return;

            _leftBorder.Destroy();
            _rightBorder.Destroy();

            Destroy(gameObject);
        }
    }
}