using Godot;
using System;
using System.Linq;
using System.Reflection.Metadata;

public abstract partial class Character : CharacterBody2D
{
	[Export] private StatResource[] stats;

    [ExportGroup("Required nodes")]
	[Export] public AnimatedSprite2D AnimSpriteNode { get; private set; }
	[Export] public StateMachine StateMachineNode { get; private set; }
	[Export] public Area2D HurtboxNode { get; private set; }
	[Export] public Area2D HitboxNode { get; private set; }
	[Export] public CollisionShape2D HitBoxShapeNode { get; private set; }
	[Export] public Timer shaderTimerNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Path2D PathNode { get; private set; }
	[Export] public NavigationAgent2D NavAgentNode { get; private set; }
	[Export] public Area2D ChaseAreaNode { get; private set;}
	[Export] public Area2D AttackAreaNode { get; private set;}

    [Export] public float speed = 100f;
	public Vector2 direction = new();
	private ShaderMaterial shader;

	public override void _Ready()
	{
		base._Ready();

		shader = (ShaderMaterial)AnimSpriteNode.Material;

		if (HurtboxNode != null)
		{
			GD.Print($"{this.Name} has hurtbox");
		}
		else
		{
			GD.Print($"{this.Name} does not have hurtbox");
		}
		HurtboxNode.AreaEntered += HandleHurtboxEntered;

		shaderTimerNode.Timeout += HandleShaderTimerTimeout;
	
	}

    private void HandleShaderTimerTimeout()
    {
        shader.SetShaderParameter(
			"active", false
		);
    }

    private void HandleHurtboxEntered(Area2D area)
	{
		StatResource health = GetStatResource(Stat.Health);
	
		Character attacker = area.GetOwner<Character>();
	
		GD.Print("Character: " + attacker.Name);
		
		if (health != null)
		{
			health.StatValue -= attacker.GetStatResource(Stat.Strength).StatValue;
			GD.Print(health.StatValue);
		}
		else
		{
			GD.Print($"{this.Name} health is null");
		}

		shader.SetShaderParameter(
			"active", true
		);

		shaderTimerNode.Start();
	}

	public StatResource GetStatResource(Stat stat)
	{
		return stats.Where((element) => element.StatType == stat).FirstOrDefault();
	}

    public void Flip()
	{
		bool isNotMovingHorizontally = (Velocity.X == 0);

		if (isNotMovingHorizontally) { return; }

		bool isMovingLeft = Velocity.X < 0;

		AnimSpriteNode.FlipH = isMovingLeft;
	}

	public void ToggleHitbox(bool enable)
	{
		HitBoxShapeNode.Disabled = !enable;
	}
}