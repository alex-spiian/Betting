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
        MultipliersSpawner multipliersSpawner,
        IInputHandler inputHandler,
        MainScreen mainScreen)
    {
        _playerInitialConfig = playerInitialConfig;
        _playerModel = playerModel;
        _inputHandler = inputHandler;
        _mainScreen = mainScreen;
        _multipliersSpawner = multipliersSpawner;
        _bettingSystem = new BettingSystem(_playerModel, _inputHandler);

        _inputHandler.BetAmountChanged += OnBetChanged;
        _playerModel.Wallet.MoneyChanged += OnMoneyChanged;
        _subscriptions.Add(Router.Default.Subscribe<GotTargetEvent>(OnGotTarget));
        _subscriptions.Add(Router.Default.Subscribe<NotEnoughMoneyEvent>(OnNotEnoughMoney));
    }
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _playerModel.Initialize(_playerInitialConfig.Money);
        _multipliersSpawner.Generate();
    }
    
    public void OnEnter()
    {
        ScreensManager.OpenScreen<MainScreen>();
    }
    
    public void Dispose()
    {
        _inputHandler.BetAmountChanged -= OnBetChanged;
        _playerModel.Wallet.MoneyChanged -= OnMoneyChanged;
        _subscriptions?.Dispose();
    }

    private void OnBetChanged(float amount)
    {
        _mainScreen.RefreshBetAmount(amount);
        if (!_playerModel.Wallet.HasEnoughMoney(amount))
        {
            _stateMachine.Enter<PaymentState, BettingSystem>(_bettingSystem);
        }
    }

    private void OnMoneyChanged(float amount)
    {
        _mainScreen.RefreshMoneyAmount(amount);
    }

    private void OnGotTarget(GotTargetEvent eventData, PublishContext publishContext)
    {
        _playerModel.Wallet.AddFunds(eventData.Multiplier, eventData.CurrentBet);
    }

    private void OnNotEnoughMoney(NotEnoughMoneyEvent eventData, PublishContext publishContext)
    {
        _stateMachine.Enter<PaymentState, BettingSystem>(_bettingSystem);
    }
}