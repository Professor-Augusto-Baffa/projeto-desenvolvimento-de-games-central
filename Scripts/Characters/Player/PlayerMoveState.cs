using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerIdleState>(); // Troca para o estado de idle
            return; // Retorna para não executar o resto
        }

        MoveCharacter();
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>(); // Troca para o estado de dash
        }
    }

    protected override void EnterState()
    {
        base.EnterState();
        
        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_MOVE); // Toca a animação de move
    }
}
