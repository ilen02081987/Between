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

        [SerializeField] private Collider _leftBorder;
        [SerializeField] private Collider _rightBorder;

        private float _playerPositionX => Player.Instance.Controller.Position.x;
        private float _playerWidth;
        private bool _isEnabled = false;

        private void Start()
        {
            var characterController = Player.Instance.Controller.GetComponent<CharacterController>();
            _playerWidth = characterController.radius + characterController.skinWidth;
        }

        private void Update()
        {
            if (Player.Instance.Controller == null)
                return;

            if (_playerPositionX - _playerWidth > _leftBorder.transform.position.x
                && _playerPositionX + _playerWidth < _rightBorder.transform.position.x)
                EnableBorders();
        }

        public void AddEnemy(BaseEnemy enemy)
        {
            _enemies.Add(enemy);

            if (_isEnabled)
                enemy.OnDie += () => TryDestroyBorders(enemy);
        }

        private void EnableBorders()
        {
            _leftBorder.isTrigger = false;
            _rightBorder.isTrigger = false;

            foreach (var enemy in _enemies)
            {
                enemy.OnDie += () => TryDestroyBorders(enemy);
            }

            _isEnabled = true;
            OnActivate?.Invoke();
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

            Destroy(_leftBorder.gameObject);
            Destroy(_rightBorder.gameObject);
            Destroy(gameObject);
        }
    }
}