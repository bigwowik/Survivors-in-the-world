using System;

namespace CodeBase.Infrastructure.Difficulty
{
    public interface IDifficultyService
    {
        float EnemySpawnWaitTime();
        int GetUpgradePrice();
        void CompleteUpgrade();
        event Action UpgradeWasCompleted;
        float EnemyMaxHpValue();
        void EnemyIncreaseCounter();
    }

}