namespace Core
{
    public class BootstrapEntryPoint : Zenject.IInitializable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly BettingState _bettingState;
        private readonly PaymentState _paymentState;

        private StateMachine _gameStateMachine;
        private BettingSystem _bettingSystem;

        public BootstrapEntryPoint(
            ProjectileSpawner projectileSpawner, 
            IInputHandler inputHandler,
            BettingState bettingState,
            PaymentState paymentState)
        {
            _paymentState = paymentState;
            _bettingState = bettingState;
            _projectileSpawner = projectileSpawner;
            _inputHandler = inputHandler;
        }

        public void Initialize()
        {
            CreateGameStateMachine();

            _inputHandler.Initialize();
            _projectileSpawner.Initialize(_inputHandler);
        }

        private void CreateGameStateMachine()
        {
            _gameStateMachine = new StateMachine(
                _bettingState,
                _paymentState
            );
            
            _gameStateMachine.Initialize();
            _gameStateMachine.Enter<BettingState>();
        }
    }
}