using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        CheckForMoveInput();
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

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_IDLE); // Toca a animação de idle
    }
}
