using System;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Difficulty
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IStaticDataService _staticDataService;
        
        private int _enemySpawned;
        private int _upgradesCount;
        public event Action UpgradeWasCompleted;

        public DifficultyService(IStaticDataService staticDataService) => 
            _staticDataService = staticDataService;

        public int GetUpgradePrice()
        {
            int currentUpgradePrice = (int) (_staticDataService.GetUpgradeStaticData().StartUpgradePrice *
                                             (Mathf.Pow(_upgradesCount + 1,
                                                 _staticDataService.GetUpgradeStaticData().UpgradePriceIncreaser)));

            return currentUpgradePrice;
        }

        public void CompleteUpgrade()
        {
            _upgradesCount++;
            UpgradeWasCompleted?.Invoke();
        }

        public float EnemySpawnWaitTime()
        {
            float currentEnemySpawnTime = GetCurrentSpawnTime(
                _enemySpawned,
                LevelStaticData.StartEnemySpawnRepeatTime,
                LevelStaticData.SpawnIncreaser);

            return currentEnemySpawnTime;
        }

        public float EnemyMaxHpValue()
        {
            float hp = GetCurrentEnemyHp(_enemySpawned,
                LevelStaticData.StartEnemyHp,
                LevelStaticData.EnemyHpIncreaser);

            return hp;
        }

        public void EnemyIncreaseCounter() => 
            _enemySpawned++;

        public void Reset()
        {
            _enemySpawned = 0;
            _upgradesCount = 0;
            UpgradeWasCompleted?.Invoke();
        }

        private LevelStaticData LevelStaticData =>
            _staticDataService.ForLevel(SceneManager.GetActiveScene().name);


        private float GetCurrentSpawnTime(int enemySpawned, float startSpawnValue, float increaser)
        {
            float spawnTime = startSpawnValue
                              * (1 / (enemySpawned * increaser + 1)); //some balance formula, empirical
            
            return spawnTime;
        }

        private float GetCurrentEnemyHp(int enemySpawned, float startHp, float increaser)
        {
            float hp = startHp
                       * ((Mathf.Pow(enemySpawned + 1, increaser))); //some balance formula, empirical

            return hp;
        }
    }
}