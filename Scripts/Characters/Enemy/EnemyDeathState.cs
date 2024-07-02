using Godot;
using System;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimSpriteNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        characterNode.PathNode.QueueFree(); // Deleta o nó do personagem e todos os seus filhos

        GameEvents.RaiseEnemyDied((characterNode as Enemy).Reward);
    }
}
