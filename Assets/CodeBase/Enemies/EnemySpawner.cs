using System.Collections;
using System.Numerics;
using CodeBase.Hero;
using CodeBase.Infrastracture;
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
        private IStaticDataService _staticDataService;
        private LevelStaticData _levelStaticData;


        [Inject]
        private void Construct(IGameFactory gameFactory, ICoroutineRunner coroutineRunner, HeroMove heroMove, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _coroutineRunner = coroutineRunner;
            _heroMove = heroMove;
            _staticDataService = staticDataService;
        }

        public void StartSpawnEnemy()
        {
            //_gameFactory.Load();

            _levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            
            _coroutineRunner.StartCoroutine(SpawnEnemyWaves());
        }

        public IEnumerator SpawnEnemyWaves() 
        {
            while (SpawnCondition())
            {
                SpawnEnemies();
                yield return new WaitForSeconds(_levelStaticData.StartEnemySpawnRepeatTime);
            }
        }

        public void SpawnEnemies()
        {
            Vector2 position = (Vector2) _heroMove.transform.position + Helper.RandomInCircle(_minRange, _maxRange);
            _gameFactory.CreateEnemy(EnemyType.Ork, position);
            Debug.Log("Enemy from wave was spawned");
        }

        private bool SpawnCondition()
        {
            return _heroMove != null;
        }
    }
}