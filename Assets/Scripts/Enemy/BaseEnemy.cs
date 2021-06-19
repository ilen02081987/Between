using Between.Damage;
using Between.Teams;
using System;
using UnityEngine;

namespace Between.Enemies
{
    public class BaseEnemy : BaseDamagableObject
    {
        public event Action OnAttack;
        public event Action OnDamage;
        public event Action OnDie;
        public event Action OnMove;
        public override Team Team => Team.Enemies;

        [SerializeField] private float _destroyTime = 2;

        protected void InvokeAttackEvent() => OnAttack?.Invoke();
        protected void InvokeMoveEvent() => OnMove?.Invoke();

        protected override void PerformOnDamage()
        {
            OnDamage?.Invoke();
        }

        protected override void PerformOnDie()
        {
            Destroy(gameObject, _destroyTime);
            OnDie?.Invoke();
        }
    }
}