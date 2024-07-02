using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyPatrolState : EnemyState
{
    [ExportGroup("Idle Time")]
    [Export] private Timer idleTimerNode;
    [Export] private float maxIdleTime = 4.0f;
    [Export] private float minIdleTime = 0.2f;
    
    private int pointIndex =0;
    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_MOVE);
        
        pointIndex = 1;
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.NavAgentNode.TargetPosition = destination;

        characterNode.NavAgentNode.NavigationFinished += HandleNavigationFinished;
        idleTimerNode.Timeout += HandleIdleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        base.ExitState();

        characterNode.NavAgentNode.NavigationFinished -= HandleNavigationFinished;
        idleTimerNode.Timeout -= HandleIdleTimerTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!idleTimerNode.IsStopped())
        {
            return;
        }

        Move();
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_IDLE);

        RandomNumberGenerator rng = new();
        idleTimerNode.WaitTime = rng.RandfRange(0.2f, maxIdleTime);


        idleTimerNode.Start();
    }

    private void HandleIdleTimerTimeout()
    {
        characterNode.AnimSpriteNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = Mathf.Wrap(pointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);

        destination = GetPointGlobalPosition(pointIndex);
        characterNode.NavAgentNode.TargetPosition = destination;
    }
}
