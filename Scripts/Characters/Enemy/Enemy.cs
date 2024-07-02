using Godot;
using System;

public partial class Enemy : Character
{
    private float strengthMultiplier = 1.5f;
    [Export ]public int Reward { get; private set; } = 10;

    public override void _Ready()
    {
        base._Ready();

        GameEvents.OnNightEnter += HandleNightEnter;
        GameEvents.OnNightExit += HandleNightExit;
    }

    private void HandleNightExit()
    {
        GetStatResource(Stat.Strength).StatValue /= strengthMultiplier;
    }

    private void HandleNightEnter()
    {
        GetStatResource(Stat.Strength).StatValue *= strengthMultiplier;
    }
}
