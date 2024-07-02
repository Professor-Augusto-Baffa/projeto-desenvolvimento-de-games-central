using Godot;
using System;

public partial class Camera : Camera2D
{
    [Export] private Node target;
    [Export] private Vector2 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleStartGame()
    {
        Reparent(target);

        Position = positionFromTarget;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }
}
