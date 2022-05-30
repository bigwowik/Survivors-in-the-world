using System.Collections.Generic;
using System.Linq;
using CodeBase.Hero;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastracture.States
{
    public class LoadLevelState : IState
    {
        private const string SceneName = "Main";

        private readonly IGameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private IEnemyFactory _enemyFactory;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IEnemyFactory enemyFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _enemyFactory = enemyFactory;
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
            //SpawnHero


            //spawn
            SpawnEnemies();


            //loadprogress


            _gameStateMachine.Enter<GameLoopState>();
        }

        private void SpawnEnemies()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnMarkers").ToList();

            if (Helper.GetArrayOfTypeByGameObjects<EnemyMarker>(spawnPoints, out List<EnemyMarker> enemyMarkers))
            {
                _enemyFactory.Load();

                foreach (var enemyMarker in enemyMarkers)
                {
                    _enemyFactory.Create(enemyMarker.EnemyType, enemyMarker.transform.position);
                }
            }
        }
    }
}