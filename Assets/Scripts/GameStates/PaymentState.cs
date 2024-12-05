using System;
using Zenject;

public class PaymentState : IPayLoadedState<BettingSystem>, IDisposable
{
    private StateMachine _stateMachine;
    private BettingSystem _bettingSystem;
    private IInputHandler _inputHandler;
    private PaymentScreen _paymentScreen;

    [Inject]
    public void Construct(IInputHandler inputHandler, PaymentScreen paymentScreen)
    {
        _paymentScreen = paymentScreen;
        _inputHandler = inputHandler;
        _inputHandler.BalanceToppedUp += OnBalanceToppedUp;
    }
    
    public void Dispose()
    {
        _inputHandler.BalanceToppedUp -= OnBalanceToppedUp;
    }
    

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter(BettingSystem bettingSystem)
    {
        _bettingSystem = bettingSystem;
        ScreensManager.OpenScreen<WarningScreen>();
    }

    private void OnBalanceToppedUp(int amount)
    {
        var isSuccessful = _bettingSystem.TryValidateRecharging(amount);
        _paymentScreen.OnPaymentValidated(isSuccessful);

        if (isSuccessful)
        {
            SetBettingState();
        }
    }
    
    private void SetBettingState()
    {
        ScreensManager.CloseScreen<WarningScreen>();
        _stateMachine.Enter<BettingState>();
    }
}