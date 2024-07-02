using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private float speed = 150f;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimerTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    private void HandleDashTimerTimeout()
    {
        characterNode.Velocity = Vector2.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>(); // Troca para o estado de idle
    }

    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_DASH); // Toca a animação de dash
        characterNode.Velocity = new(characterNode.direction.X, characterNode.direction.Y); // Define a direção do dash
        
        // Se o personagem estiver parado, define a direção do dash para a direção que ele está olhando
        if (characterNode.Velocity == Vector2.Zero) {
            characterNode.Velocity = characterNode.AnimSpriteNode.FlipH ? Vector2.Left : Vector2.Right;
        }
        
        characterNode.Velocity *= speed;
        dashTimerNode.Start();
    }
}
