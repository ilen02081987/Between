using Between.Interfaces;
using Between.Teams;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamagable
{
    public abstract Team Team { get; set; }

    public event Action OnAttack;
    public event Action OnDamage;
    public event Action OnDie;
    public event Action OnMove;

    [SerializeField] private float _health;
    [SerializeField] private float _destroyTime = 2f;

    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject, _destroyTime);
            OnDie?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }
    }

    protected void InvokeAttackEvent() => OnAttack?.Invoke();
    protected void InvokeDieEvent() => OnDie?.Invoke();
    protected void InvokeDamageEvent() => OnDamage?.Invoke();
    protected void InvokeMoveEvent() => OnMove?.Invoke();
}
