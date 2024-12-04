public class PaymentState : IState
{
    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        ScreensManager.OpenScreen<WarningScreen>();
    }

    public void OnBalanceToppedUp()
    {
        ScreensManager.CloseScreen<WarningScreen>();
    }
}