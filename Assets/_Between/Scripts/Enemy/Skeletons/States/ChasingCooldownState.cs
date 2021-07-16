using Between.Animations;
using Between.Enemies.Skeletons;
using Between.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class ChasingCooldownState : ChasingState
{
    private readonly float _baseCooldown;
    private float _cooldown;

    protected override bool CanAttack => _cooldown <= 0f;

    public ChasingCooldownState(FinitStateMachine stateMachine, SkeletonData data) : base(stateMachine, data)
    {
        _baseCooldown = data.CooldownTime;
    }

    public override void Enter()
    {
        _cooldown = _baseCooldown;
        base.Enter();
    }

    public override void Update()
    {
        _cooldown -= Time.deltaTime;
        base.Update();
    }

    public override void Exit()
    {
        _cooldown = _baseCooldown;
        base.Exit();
    }
}