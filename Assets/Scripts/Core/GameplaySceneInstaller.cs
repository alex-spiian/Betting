using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private MultipliersSpawner _multipliersSpawner;
        [SerializeField] private ProjectileSpawner _projectileSpawner;
        [SerializeField] private InputHandler _inputHandler;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle();

            Container.Bind<MultipliersSpawner>().FromInstance(_multipliersSpawner);
            Container.Bind<ProjectileSpawner>().FromInstance(_projectileSpawner);
            Container.Bind<IInputHandler>().FromInstance(_inputHandler);
        }
    }
}