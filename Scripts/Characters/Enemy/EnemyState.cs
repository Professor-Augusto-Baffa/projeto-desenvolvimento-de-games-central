using System;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector2 destination;

    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }


    protected Vector2 GetPointGlobalPosition(int index)
    {
        Vector2 localPos = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector2 globalPos = characterNode.PathNode.GlobalPosition;
        return globalPos + localPos;
    }

    protected void Move()
    {
        characterNode.NavAgentNode.GetNextPathPosition(); // Atualiza a posição do destino do NavAgent
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(destination) * characterNode.speed;
        
        characterNode.MoveAndSlide();
        characterNode.Flip();
        characterNode.AnimSpriteNode.FlipH = !characterNode.AnimSpriteNode.FlipH;
    }

    protected void HandleChaseAreaBodyEntered(Node2D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}