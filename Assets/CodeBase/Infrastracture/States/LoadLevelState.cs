using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastracture.States
{
    public class LoadLevelState : IState
    {
        private const string SceneName = "Level1";

        private readonly IGameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;

        private EnemySpawner _enemySpawnerInstance;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            IGameFactory gameFactory, 
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService
            )
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
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
            
            _staticDataService.LoadData();
            
            CreateHero();
            CreateHud();
            CreateSpawner();


            //loadprogress


            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateSpawner()
        {
            _gameFactory.CreateEnemySpawner();
        }

        private void CreateHud() => 
            _gameFactory.CreateHud();

        private void CreateHero()
        {
            Vector2 heroStartPoint = Vector2.zero;
            _gameFactory.CreateHero(heroStartPoint);
        }

    }
}