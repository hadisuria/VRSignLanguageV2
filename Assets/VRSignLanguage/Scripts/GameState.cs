public static class GameState
{
    public enum state
    {
        Beginner,
        Memorized,
        Dictionary,
        Stop
    }

    public static state currState { get; private set; } = state.Stop;
    public static state prevState { get; private set; } = state.Stop;
    public static bool isEnemyHitPlayer { get; private set; } = false;
    public static bool isGestureCorrect { get; private set; } = false;

    public static void SetCurrState(state targetState)
    {
        prevState = currState;
        currState = targetState;
    }

    public static void SetIsEnemyHitPlayer(bool value)
    {
        isEnemyHitPlayer = value;
    }

    public static void SetIsGestureCorrect(bool value)
    {
        isGestureCorrect = value;
    }
}
