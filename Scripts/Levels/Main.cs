using Godot;
using System;

public partial class Main : Node2D
{
    public override void _Ready()
    {
        GetTree().Paused = true;
    }
}