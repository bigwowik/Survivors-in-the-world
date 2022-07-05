using CodeBase.Infrastructure.Inputs;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.Restart;
using CodeBase.Infrastructure.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindGame();
            BindGameStateMachine();
            
            BindCoroutineRunner();
            BindInputService();
            BindSceneLoader();
            BindRestartService();
        }

        private void BindGame()
        {
            Container
                .Bind<Game>()
                .AsSingle().NonLazy();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<UnityInputService>()
                .AsSingle();
        }

        private void BindRestartService()
        {
            Container
                .Bind<IRestartService>()
                .To<RestartService>()
                .AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }
    }
}