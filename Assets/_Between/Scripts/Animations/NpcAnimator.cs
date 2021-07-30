using System;
using UnityEngine;
using Between.Enemies;

namespace Between.Animations
{
    [RequireComponent(typeof(Animator))]
    public class NpcAnimator : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        [SerializeField] private float _moveAnimationSpeed = 1;
        [SerializeField] private float _takeDamageAnimationSpeed = 1;
        [SerializeField] private float _attackAnimationSpeed = 1;
        [SerializeField] private float _dieAnimationSpeed = 1;
        [SerializeField] private float _defendAnimationSpeed = 1;

        private BaseEnemy _npc;
        private Animator _animator;

        private Action _onAnimationAttack;
        private Action _onCompleteTakeDamage;
        private Action _onCompleteDefend;

        public void AttachTo(BaseEnemy npc)
        {
            _npc = npc;
            _npc.OnDie += Die;
            
            _animator = GetComponent<Animator>();
        }

        internal void Defend(Action onComplete)
        {
            _onCompleteDefend = onComplete;

            _animator.speed = _defendAnimationSpeed;
            _animator.SetTrigger("Defend");
        }

        private void OnDestroy()
        {
            if (_npc != null)
                _npc.OnDie -= Die;
        }

        public void StartMove() => _animator.SetTrigger("StartMove");
        public void Move(float animationSpeed)
        {
            _animator.speed = _moveAnimationSpeed;
            _animator.SetFloat("Move", animationSpeed);
        }

        public void Attack(Action onAnimationAttack)
        {
            _onAnimationAttack = onAnimationAttack;

            _animator.speed = _attackAnimationSpeed;
            _animator.SetTrigger("Attack");
        }

        public void TakeDamage(Action onComplete)
        {
            _onCompleteTakeDamage = onComplete;

            _animator.speed = _takeDamageAnimationSpeed;
            _animator.SetTrigger("TakeDamage");
        }

        private void Die()
        {
            _animator.speed = _dieAnimationSpeed;
            _animator.SetTrigger("Die");
        }
        
        private void PerformOnTakeDamageComplete()
        {
            _onCompleteTakeDamage?.Invoke();
            _onCompleteTakeDamage = null;
        }

        private void PerformOnAnimationAttack()
        {
            _onAnimationAttack?.Invoke();
            _onAnimationAttack = null;
        }

        private void PerformOnDefendComplete()
        {
            _onCompleteDefend?.Invoke();
            _onCompleteDefend = null;
        }
    }
}