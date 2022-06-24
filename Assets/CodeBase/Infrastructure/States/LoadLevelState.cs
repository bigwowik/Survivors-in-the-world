using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Loading;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string SceneName = "Level1";

        private readonly IGameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;

        private EnemySpawner _enemySpawnerInstance;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory
            )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }
        public void Enter()
        {
            _sceneLoader.Load(SceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        void OnLoaded()
        {
            Debug.Log("LoadLevelState - On loaded");
            
            _gameStateMachine.Enter<GameLoopState>();
        }

    }
}