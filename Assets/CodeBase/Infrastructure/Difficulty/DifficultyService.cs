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

        private LevelStaticData _levelStaticData =>
            _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

        public DifficultyService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public int GetUpgradePrice()
        {
            int currentUpgradePrice = (int) (_staticDataService.GetUpgradeStaticData().StartUpgradePrice *
                                             (Mathf.Pow(_upgradesCount + 1,
                                                 _staticDataService.GetUpgradeStaticData().UpgradePriceIncreaser)));

            Debug.Log($"_upgradesCount: {_upgradesCount}. currentUpgradePrice: {currentUpgradePrice}");
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
                _levelStaticData.StartEnemySpawnRepeatTime,
                _levelStaticData.SpawnIncreaser);

            return currentEnemySpawnTime;
        }

        public float EnemyMaxHpValue()
        {
            float hp = GetCurrentEnemyHp(_enemySpawned,
                _levelStaticData.StartEnemyHp,
                _levelStaticData.EnemyHpIncreaser);

            return hp;
        }
        
        public void EnemyIncreaseCounter()
        {
            _enemySpawned++;
        }


        private float GetCurrentSpawnTime(int enemySpawned, float startSpawnValue, float increaser)
        {
            float spawnTime = startSpawnValue
                              * (1 / (enemySpawned * increaser + 1)); //some balance formula, empirical

            Debug.Log($"EnemySpawned: {enemySpawned}. SpawnTime: {spawnTime}");
            return spawnTime;
        }

        private float GetCurrentEnemyHp(int enemySpawned, float startHp, float increaser)
        {
            float hp = startHp
                       * ((Mathf.Pow(enemySpawned + 1, increaser))); //some balance formula, empirical

            Debug.Log($"EnemySpawned: {enemySpawned}. Enemy HP: {(int)hp}");
            return hp;
        }
    }
}