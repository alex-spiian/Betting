namespace Core
{
    public class BootstrapEntryPoint : Zenject.IInitializable
    {
        private readonly IInputHandler _inputHandler;
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly BettingState _bettingState;
        private readonly PaymentState _paymentState;
        private readonly ShapeSpawner _shapeSpawner;
        private readonly MultipliersSpawner _multipliersSpawner;

        private StateMachine _gameStateMachine;
        private BettingSystem _bettingSystem;

        public BootstrapEntryPoint(
            ProjectileSpawner projectileSpawner,
            ShapeSpawner shapeSpawner,
            MultipliersSpawner multipliersSpawner,
            IInputHandler inputHandler,
            BettingState bettingState,
            PaymentState paymentState)
        {
            _multipliersSpawner = multipliersSpawner;
            _shapeSpawner = shapeSpawner;
            _paymentState = paymentState;
            _bettingState = bettingState;
            _projectileSpawner = projectileSpawner;
            _inputHandler = inputHandler;
        }

        public void Initialize()
        {
            CreateGameStateMachine();

            _shapeSpawner.Spawn();
            _inputHandler.Initialize();
            _projectileSpawner.Initialize(_inputHandler);
            _multipliersSpawner.Initialize(_shapeSpawner.GetMultiplierBundles());
            _multipliersSpawner.Generate();
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