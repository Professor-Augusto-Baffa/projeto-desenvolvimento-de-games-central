using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private Vector2 targetPosition;
    private int attackFrame = 3;

    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_ATTACK);

        Node2D target = characterNode.AttackAreaNode.GetOverlappingBodies().First(); // Pega o primeiro corpo que colidiu com a área de ataque

        targetPosition = target.GlobalPosition;

        characterNode.AnimSpriteNode.FrameChanged += HandleFrameChanged;
        characterNode.AnimSpriteNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        base.ExitState();

        characterNode.AnimSpriteNode.FrameChanged -= HandleFrameChanged;
        characterNode.AnimSpriteNode.AnimationFinished -= HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        characterNode.ToggleHitbox(false);

        Node2D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            Node2D chaseTarget = characterNode.ChaseAreaNode
                .GetOverlappingBodies()
                .FirstOrDefault();
            
            if (chaseTarget != null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
                return;
            }
            characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
            return;
        }

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;
        

    }

    private void HandleFrameChanged()
    {
        if (characterNode.AnimSpriteNode.Frame == attackFrame)
        {
            PerformHit();
        }
    }

    public void PerformHit()
    {
        characterNode.HitboxNode.GlobalPosition = targetPosition;
        
        characterNode.AnimSpriteNode.FlipH = targetPosition.X > characterNode.GlobalPosition.X; // Inverte o sprite se o alvo estiver atrás do inimigo
        // characterNode.AnimSpriteNode.FlipH = characterNode.GlobalPosition.DirectionTo(targetPosition).X < 0;
        characterNode.ToggleHitbox(true);

    }
    
}
