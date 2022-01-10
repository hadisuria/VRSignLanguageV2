public static class GameState
{
    public enum state
    {
        Play,
        Stop
    }

    public static state currState { get; private set; } = state.Stop;

    public static void SetCurrState(state targetState)
    {
        currState = targetState;
    }
}
