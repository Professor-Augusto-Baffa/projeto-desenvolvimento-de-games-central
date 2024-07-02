using Godot;
using System;
using System.Diagnostics;

public partial class Player : Character
{
	[Export] private PointLight2D lightNode;

	public override void _Ready()
	{
		base._Ready();
		lightNode ??= GetChild<PointLight2D>(0);

		GameEvents.OnNightEnter += HandleNightEnter;
		GameEvents.OnNightExit += HandleNightExit;
		GameEvents.OnEnemyDeath += HandleEnemyDeath;
	}

    private void HandleNightExit()
    {
        lightNode.Enabled = false;
    }

    private void HandleNightEnter()
    {
        lightNode.Enabled = true;
    }

    public override void _Input(InputEvent @event)
	{
		direction = Input.GetVector(
			GameConstants.INPUT_MOVE_LEFT,
			GameConstants.INPUT_MOVE_RIGHT,
			GameConstants.INPUT_MOVE_FORWARD,
			GameConstants.INPUT_MOVE_BACKWARD
		);
	}

	private void HandleEnemyDeath(int reward)
	{
		GetStatResource(Stat.Gold).StatValue += reward;
	}
}
