using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_IDLE);
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();

        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }
}
