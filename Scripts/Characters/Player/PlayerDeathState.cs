using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimSpriteNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished()
    {
        GameEvents.RaiseEndGame();

        characterNode.QueueFree(); // Deleta o nó do personagem e todos os seus filhos
    }
}
