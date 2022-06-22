using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastracture.Difficulty
{
    public class DifficultyService : IDifficultyService, IInitializable
    {
        private readonly IStaticDataService _staticDataService;

        private float _currentEnemySpawnTime;
        private int _enemySpawned;
        private LevelStaticData _levelStaticData => _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

        public DifficultyService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            
        }

        public float EnemySpawnWaitTime()
        {
            _currentEnemySpawnTime = GetCurrentSpawnTime(
                _enemySpawned++,
                _levelStaticData.StartEnemySpawnRepeatTime,
                _levelStaticData.SpawnIncreaser);

            return _currentEnemySpawnTime;
        }

        private void Init()
        {
            //_levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
        }


        private float GetCurrentSpawnTime(int enemySpawned, float startSpawnValue, float increaser)
        {
            float spawnTime = startSpawnValue
                        * (1/(enemySpawned * increaser + 1)); //some balance formula, empirical
            
            Debug.Log($"EnemySpawned: {enemySpawned}. SpawnTime: {spawnTime}");
            return spawnTime;
        }

        public void Initialize()
        {
            //Init();
        }
    }
}