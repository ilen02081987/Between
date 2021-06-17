using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Animations
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private BaseEnemy _skeletonMelee;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _skeletonMelee.OnMove += Move;
            _skeletonMelee.OnAttack += Attack;
            _skeletonMelee.OnDamage += TakeDamage;
            _skeletonMelee.OnDie += Die;
        }

        private void OnDestroy()
        {
            _skeletonMelee.OnMove -= Move;
            _skeletonMelee.OnAttack -= Attack;
            _skeletonMelee.OnDamage -= TakeDamage;
            _skeletonMelee.OnDie -= Die;
        }

        private void Move()
        {
            _animator.SetTrigger("Move");
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void TakeDamage()
        {
            _animator.SetTrigger("TakeDamage");
        }

        private void Die()
        {
            _animator.SetTrigger("Die");
        }
    }
}