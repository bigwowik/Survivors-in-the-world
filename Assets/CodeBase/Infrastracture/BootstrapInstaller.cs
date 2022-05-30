﻿using CodeBase.Hero;
using CodeBase.Infrastracture.States;
using Zenject;

namespace CodeBase.Infrastracture
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
            BindEnemyFactory();
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
            Container
                .Bind<SceneLoader>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
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