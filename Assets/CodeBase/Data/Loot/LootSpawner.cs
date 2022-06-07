using CodeBase.Infrastracture;
using CodeBase.Stats;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;
        //private IRandomService _random;
        private int _lootMin;
        private int _lootMax;


        [Inject]
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
            //_random = random;
        }

        void Start()
        {
            EnemyDeath.OnDeathEvent += SpawnLoot;
        }

        private void SpawnLoot(EnemyDeath destroyedObject, GameObject destroyer)
        {
            LootItem loot = _factory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            loot.Initialize(lootItem);
        }


        private Loot GenerateLoot()
        {
            return new Loot(
                LootType.MONEY, 
                1);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}