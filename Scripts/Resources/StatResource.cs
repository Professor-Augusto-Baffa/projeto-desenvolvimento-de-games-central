using Godot;
using System;

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZero;
    public event Action OnUpdate;
    
    [Export] public Stat StatType { get; private set; }

    private float _statValue; // valor do stat, para que não gere uma recursão infinita, não é possível chamar StatValue diretamente
    [Export] public float StatValue 
    { 
        get => _statValue;
        set
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            OnUpdate?.Invoke();

            if (_statValue == 0)
            {
                OnZero?.Invoke();
            }
        }
    }
}