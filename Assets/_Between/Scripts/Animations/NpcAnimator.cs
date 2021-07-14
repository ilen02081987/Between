using System;
using UnityEngine;
using Between.Enemies;

namespace Between.Animations
{
    [RequireComponent(typeof(Animator))]
    public class NpcAnimator : MonoBehaviour
    {
        private BaseEnemy _npc;
        private Animator _animator;

        private Action _onAnimationAttack;
        private Action _onCompleteTakeDamage;

        public void AttachTo(BaseEnemy npc)
        {
            _npc = npc;
            _npc.OnDie += Die;
            
            _animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            _npc.OnDie -= Die;
        }

        public void StartMove() => _animator.SetTrigger("StartMove");
        public void Move(float animationSpeed)
        {
            _animator.SetFloat("Move", animationSpeed);
        }

        public void Attack(Action onAnimationAttack)
        {
            _onAnimationAttack = onAnimationAttack;
            _animator.SetTrigger("Attack");
        }

        public void TakeDamage(Action onComplete)
        {
            _onCompleteTakeDamage = onComplete;
            _animator.SetTrigger("TakeDamage");
        }

        private void PerformOnTakeDamageComplete()
        {
            _onCompleteTakeDamage?.Invoke();
        }

        private void PerformOnAnimationAttack()
        {
            _onAnimationAttack?.Invoke();
        }

        private void Die()
        {
            _animator.SetTrigger("Die");
        }
    }
}