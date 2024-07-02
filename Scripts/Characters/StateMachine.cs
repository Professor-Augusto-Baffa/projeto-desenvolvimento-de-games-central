using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState; // estado atual
    [Export] private Node[] states; // lista de estados

    public override void _Ready()
    {
        // Notifica o estado atual
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>() // Troca de estado
{
        // Procura o estado na lista de estados com o Linq da System.Linq
        Node newState = states.Where((state) => state is T)
            .FirstOrDefault();

        // Se não encontrar o estado, retorna
        if (newState == null) {
            GD.PrintErr("State not found");
            return;
        }
        
        if (currentState is T) { return;}

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE); // Notifica o estado atual (desabilita)
        currentState = newState; // Atualiza o estado atual
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE); // Notifica o novo estado (habilita)
    }
}
