using Godot;

// abstract: não pode ser instanciado, só pode ser herdado
public abstract partial class PlayerState : CharacterState
{

    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected void MoveCharacter()
    {
        characterNode.Velocity = new Vector2(characterNode.direction.X, characterNode.direction.Y);
        characterNode.Velocity *= characterNode.speed;

        characterNode.Flip();
        characterNode.MoveAndSlide();
    }

    protected void CheckForMoveInput()
    {
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>(); // Troca para o estado de movimento
        }
    }

    protected void CheckForAttackInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>(); // Troca para o estado de ataque
        }
    }

    protected void CheckForDashInput()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>(); // Troca para o estado de dash
        }
    }

    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }
}