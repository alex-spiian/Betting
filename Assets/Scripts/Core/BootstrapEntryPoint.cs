
namespace Core
{
    public class BootstrapEntryPoint : Zenject.IInitializable
    {
        private readonly MultipliersSpawner _multipliersSpawner;
        private readonly IInputHandler _inputHandler;
        private readonly ProjectileSpawner _projectileSpawner;

        private StateMachine _gameStateMachine;

        public BootstrapEntryPoint(
            ProjectileSpawner projectileSpawner, 
            MultipliersSpawner multipliersSpawner,
            IInputHandler inputHandler)
        {
            _projectileSpawner = projectileSpawner;
            _inputHandler = inputHandler;
            _multipliersSpawner = multipliersSpawner;
        }

        public void Initialize()
        {
            CreateGameStateMachine();
            
            _projectileSpawner.Initialize(_inputHandler);
            _multipliersSpawner.Generate();
        }

        private void CreateGameStateMachine()
        {
            _gameStateMachine = new StateMachine(
                new GameState(),
                new PaymentState()
            );
            
            _gameStateMachine.Initialize();
            _gameStateMachine.Enter<GameState>();
        }
    }
}