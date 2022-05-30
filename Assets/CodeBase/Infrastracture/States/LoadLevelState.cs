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
        private IFactory _factory;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IFactory factory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _factory = factory;
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
            Debug.Log("On loaded");
            //SpawnHero
            CreateHero();


            //spawn
            SpawnEnemies();


            //loadprogress


            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateHero()
        {
            Vector2 heroStartPoint = Vector2.zero;
            _factory.CreateHero(heroStartPoint);
        }


        private void SpawnEnemies()
        {
            var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnMarkers").ToList();

            if (Helper.GetArrayOfTypeByGameObjects<EnemyMarker>(spawnPoints, out List<EnemyMarker> enemyMarkers))
            {
                _factory.LoadEnemies();

                foreach (var enemyMarker in enemyMarkers)
                {
                    _factory.Create(enemyMarker.EnemyType, enemyMarker.transform.position);
                }
            }
        }
    }
}