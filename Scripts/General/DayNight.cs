using Godot;
using System;

public partial class DayNight : Node
{
    [Export] private Timer timerNode;
    [Export] private float dayDuration = 10f;
    [Export] private float nightDuration = 5f;
    [Export] private DirectionalLight2D sunNode;

    private bool isDay = true;


    public override void _Ready()
    {
        base._Ready();

        timerNode.Timeout += HandleTimerTimeout;
        
        timerNode.WaitTime = dayDuration;
        timerNode.Start();
    }

    private void HandleTimerTimeout()
    {
        if (isDay)
        {
            isDay = false;
            GameEvents.RaiseNightEnter();
            GD.Print("Night");
            sunNode.Enabled = true;
            timerNode.WaitTime = nightDuration;
        }
        else
        {
            isDay = true;
            GameEvents.RaiseNightExit();
            GD.Print("Day");
            sunNode.Enabled = false;
            timerNode.WaitTime = dayDuration;
        }

        timerNode.Start();
    }
}
