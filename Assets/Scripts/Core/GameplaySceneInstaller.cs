using UnityEngine;
using Zenject;

namespace Core
{
    
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private MultipliersSpawner _multipliersSpawner;
        [SerializeField] private ProjectileSpawner _projectileSpawner;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private MainScreen _mainScreen;
        [SerializeField] private PaymentScreen _paymentScreen;
        [SerializeField] private PlayerInitialConfig _playerInitialConfig;
        [SerializeField] private ShapeSpawner _shapeSpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<MultipliersSpawner>().FromInstance(_multipliersSpawner);
            Container.Bind<ProjectileSpawner>().FromInstance(_projectileSpawner);
            Container.Bind<IInputHandler>().FromInstance(_inputHandler);
            Container.Bind<MainScreen>().FromInstance(_mainScreen);
            Container.Bind<PaymentScreen>().FromInstance(_paymentScreen);
            Container.Bind<PlayerInitialConfig>().FromInstance(_playerInitialConfig);
            Container.Bind<ShapeSpawner>().FromInstance(_shapeSpawner);

            Container.Bind<PlayerModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<BettingState>().AsSingle();
            Container.BindInterfacesAndSelfTo<PaymentState>().AsSingle();
        }
    }
}