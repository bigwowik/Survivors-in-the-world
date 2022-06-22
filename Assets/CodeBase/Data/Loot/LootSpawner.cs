using CodeBase.Infrastructure.Factory;
using CodeBase.Stats;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _factory;


        [Inject]
        public void Construct(IGameFactory factory)
        {
            _factory = factory;
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

        
    }
}