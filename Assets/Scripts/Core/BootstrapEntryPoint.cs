
namespace Core
{
    public class BootstrapEntryPoint : Zenject.IInitializable
    {
        private readonly MultipliersSpawner _multipliersSpawner;
        private readonly IInputHandler _inputHandler;
        private readonly ProjectileSpawner _projectileSpawner;

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
            _projectileSpawner.Initialize(_inputHandler);
            _multipliersSpawner.Generate();
        }
    }
}