using Godot;
using System;
using System.Reflection.Metadata;
// using System.Numerics;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;
    private int comboCounter = 1;
    private int maxComboCount = 1;
    private int attackFrame = 2;

    public override void _Ready()
    {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        MoveCharacter();
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimSpriteNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter
        );

        characterNode.AnimSpriteNode.AnimationFinished += HandleAnimationFinished;
        characterNode.AnimSpriteNode.FrameChanged += HandleFrameChanged;
    }

    protected override void ExitState()
    {
        base.ExitState();
        characterNode.AnimSpriteNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.AnimSpriteNode.FrameChanged -= HandleFrameChanged;

        comboTimerNode.Start();
    }

    private void HandleFrameChanged()
    {
        if (characterNode.AnimSpriteNode.Frame == attackFrame)
        {
            PerformHit();
        }
        
        // TODO: Trocar para anim de andar&correr caso o jogador esteja se movendo, no mesmo frame que o ataque
    }

    private void HandleAnimationFinished()
    {
        comboCounter = Mathf.Wrap(comboCounter + 1, 1, maxComboCount + 1);

        characterNode.ToggleHitbox(false);

        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
    }

    private void PerformHit()
    {
        Vector2 newPosition = characterNode.AnimSpriteNode.FlipH ? Vector2.Left : Vector2.Right;
        float distanceMultiplier = 15f;
        newPosition *= distanceMultiplier;
        characterNode.HitboxNode.Position = newPosition;
        
        characterNode.ToggleHitbox(true);
    }
}