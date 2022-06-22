using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Data/LevelStaticData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public float StartEnemySpawnRepeatTime = 1f;
        public float SpawnIncreaser = 1f;

        public float StartEnemyHp = 1;
        public float EnemyHpIncreaser = 1.1f;

    }
}