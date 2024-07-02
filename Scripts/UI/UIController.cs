using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where(
                (element) => element is UIContainer
            ).Cast<UIContainer>().ToDictionary(
                (element) => element.container
            );

            containers[ContainerType.Start].Visible = true;

            containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
            containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
            containers[ContainerType.Victory].ButtonNode.Pressed += HandleRestarPressed;

            GameEvents.OnEndGame += HandleEndGame;
            GameEvents.OnVictory += HandleVictory;
    }

    private void HandleRestarPressed()
    {
        GetTree().ReloadCurrentScene();
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;


    }

    public override void _Input(InputEvent @event)
    {
        if (!canPause) { return; }

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE)) // se a ação de pausa não foi pressionada
        {
            return;
        }

        containers[ContainerType.Stats].Visible = GetTree().Paused; // define a visibilidade de stats como o estado de pausa, se estiver pausado, stats vira visível
        GetTree().Paused = !GetTree().Paused; // define o estado de pausa como o inverso do estado de pausa
        containers[ContainerType.Pause].Visible = GetTree().Paused; // define a visibilidade de pause como o estado de pausa
    }

    private void HandleVictory()
    {
        canPause = false;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleEndGame()
    {
        canPause = false;

        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartPressed()
    {
        canPause = true;

        GetTree().Paused = false;
        
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }
}
