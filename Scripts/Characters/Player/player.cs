using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float speed = 100f;
	
	public Vector2 direction = new();

	[Export] AnimatedSprite2D anim;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		anim.Play("idle");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Velocity = new(direction.X, direction.Y);
		Velocity *= speed;

		MoveAndSlide();
	}
	public override void _Input(InputEvent @event)
	{
		anim.Play("run");

		direction = Input.GetVector(
			"MoveLeft",
			"MoveRight",
			"MoveUp",
			"MoveDown"
		);
		if (direction == Vector2.Zero)
		{
			anim.Play("idle");
		}
		else{
			if (direction.X < 0)
			{
				anim.FlipH = true;
			}
			else if (direction.X > 0)
			{
				anim.FlipH = false;
			}
		}
	}
}
