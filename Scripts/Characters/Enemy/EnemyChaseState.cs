using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    private CharacterBody2D target;

    [Export] Timer timerNode;
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_MOVE);
        GD.Print("Entrou no estado de chase");

        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody2D; // Array do Godot != Array do C#
        
        timerNode.Timeout += HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
    }

    protected override void ExitState()
    {
        base.ExitState();

        timerNode.Timeout -= HandleTimeout;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleAttackAreaBodyEntered(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.NavAgentNode.TargetPosition = destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Move();
    }
}
