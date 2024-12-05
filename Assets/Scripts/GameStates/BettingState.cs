using System;
using VitalRouter;
using Zenject;

public class BettingState : IState, IDisposable
{
    private StateMachine _stateMachine;
    private PlayerModel _playerModel;
    private MultipliersSpawner _multipliersSpawner;
    private MainScreen _mainScreen;
    private IInputHandler _inputHandler;
    private BettingSystem _bettingSystem;

    private readonly CompositeDisposable _subscriptions = new();
    private PlayerInitialConfig _playerInitialConfig;

    [Inject]
    public void Construct(PlayerModel playerModel,
        PlayerInitialConfig playerInitialConfig,
        IInputHandler inputHandler,
        MainScreen mainScreen)
    {
        _playerInitialConfig = playerInitialConfig;
        _playerModel = playerModel;
        _inputHandler = inputHandler;
        _mainScreen = mainScreen;
        _bettingSystem = new BettingSystem(_playerModel, _inputHandler);

        _inputHandler.BetAmountChanged += OnBetChanged;
        _inputHandler.PaymentScreen += OnPaymentState;
        _playerModel.Wallet.MoneyChanged += OnMoneyChanged;
        _subscriptions.Add(Router.Default.Subscribe<GotTargetEvent>(OnGotTarget));
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerModel.Initialize(_playerInitialConfig.Money);
    }
    
    public void OnEnter()
    {
        ScreensManager.OpenScreen<MainScreen>();
    }
    
    public void Dispose()
    {
        _inputHandler.BetAmountChanged -= OnBetChanged;
        _playerModel.Wallet.MoneyChanged -= OnMoneyChanged;
        _inputHandler.PaymentScreen -= OnPaymentState;
        _subscriptions?.Dispose();
    }

    private void OnBetChanged(float amount)
    {
        _mainScreen.RefreshBetAmount(amount);
        if (!_playerModel.Wallet.HasEnoughMoney(amount))
        {
            SetPaymentState();
            ScreensManager.OpenScreen<WarningScreen>();
        }
    }

    private void OnMoneyChanged(float amount)
    {
        _mainScreen.RefreshMoneyAmount(amount);
        ValidateState();
    }

    private void OnGotTarget(GotTargetEvent eventData, PublishContext publishContext)
    {
        _playerModel.Wallet.AddFunds(eventData.Multiplier, eventData.CurrentBet);
    }

    private void ValidateState()
    {
        if (!_bettingSystem.CanBet())
        {
            SetPaymentState();
            ScreensManager.OpenScreen<WarningScreen>();
        }

        else
        {
            ScreensManager.CloseAllScreens();
        }
    }

    private void OnPaymentState()
    {
        SetPaymentState();
        ScreensManager.OpenScreen<PaymentScreen>();
    }

    private void SetPaymentState()
    {
        _stateMachine.Enter<PaymentState, BettingSystem>(_bettingSystem);
    }
}