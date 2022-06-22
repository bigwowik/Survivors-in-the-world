using System.Collections;
using System.Numerics;
using CodeBase.Hero;
using CodeBase.Infrastracture;
using CodeBase.Infrastracture.Difficulty;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace CodeBase.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _minRange = 10f;
        [SerializeField] private float _maxRange = 20f;

        private IGameFactory _gameFactory;
        private ICoroutineRunner _coroutineRunner;
        private HeroMove _heroMove;
        
        private IDifficultyService _difficultyService;


        [Inject]
        private void Construct(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, HeroMove heroMove, IDifficultyService difficultyService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _heroMove = heroMove;
            _difficultyService = difficultyService;
        }

        public void StartSpawnEnemy()
        {
            _coroutineRunner.StartCoroutine(SpawnEnemyWaves());
        }

        private IEnumerator SpawnEnemyWaves() 
        {
            while (CanSpawn())
            {
                SpawnEnemies();
                yield return new WaitForSeconds(_difficultyService.EnemySpawnWaitTime());
            }
        }

        private void SpawnEnemies()
        {
            Vector2 position = (Vector2) _heroMove.transform.position + Helper.RandomInCircle(_minRange, _maxRange);
            _gameFactory.CreateEnemy(EnemyType.Ork, position);
            Debug.Log("Enemy from wave was spawned");
        }

        private bool CanSpawn() => 
            _heroMove != null;
    }
}