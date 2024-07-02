using System;

public class GameEvents
{
    public static event Action OnStartGame;
    public static event Action OnEndGame;
    public static event Action<int> OnNewEnemyCount;
    public static event Action OnVictory;
    public static event Action OnNightEnter;
    public static event Action OnNightExit;
    public static event Action<int> OnEnemyDeath;


    public static void RaiseStartGame() => OnStartGame?.Invoke();
    public static void RaiseEndGame() => OnEndGame?.Invoke();
    public static void RaiseNewEnemyCount(int count) => OnNewEnemyCount?.Invoke(count);
    public static void RaiseVictory() => OnVictory?.Invoke();
    public static void RaiseNightEnter() => OnNightEnter?.Invoke();
    public static void RaiseNightExit() => OnNightExit?.Invoke();
    public static void RaiseEnemyDied(int reward) => OnEnemyDeath?.Invoke(reward);
}