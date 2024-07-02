using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        destination = GetPointGlobalPosition(0);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.NavAgentNode.IsNavigationFinished())
        {
            GD.Print("Chegou no destino");
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
    
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_MOVE);
        
        characterNode.NavAgentNode.TargetPosition = destination;

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();

        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
