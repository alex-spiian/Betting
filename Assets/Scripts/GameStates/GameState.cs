
public class GameState : IState
{
    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        ScreensManager.OpenScreen<MainScreen>();
    }
}