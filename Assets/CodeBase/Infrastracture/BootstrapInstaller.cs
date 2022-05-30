using CodeBase.Hero;
using CodeBase.Infrastracture.States;
using Zenject;

namespace CodeBase.Infrastracture
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindEnemyFactory();
            BindGame();
            BindGameStateMachine();
            BindCoroutineRunner();
            BindInputService();
            BindSceneLoader();
        }

        private void BindCoroutineRunner()
        {
            Container.Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
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

        private void BindSceneLoader()
        {
            //SceneLoader sceneLoader = new SceneLoader(gameBootstraper);
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<UnityInputService>()
                .AsSingle();
        }
    }
}