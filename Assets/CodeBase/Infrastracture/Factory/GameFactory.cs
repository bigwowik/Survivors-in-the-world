using System;
using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastracture.AssetsManagment;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using CodeBase.Stats;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;
using Cinemachine;
using UnityEditor.VersionControl;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastracture
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly WorldData _worldData;

        private GameObject _enemyPrefabOrk;
        private GameObject _enemyPrefabSmartOrk;

        public List<EnemyAttacker> ActiveEnemies = new List<EnemyAttacker>();
        private HeroMove _heroMove;
        private GameObject _hudInstance;
        private Projectile _projectilePrefab;
        private GameObject _warriorPrefab1;
        private GameObject _heroCameraPrefab;


        public GameFactory(DiContainer diContainer, WorldData worldData)
        {
            _diContainer = diContainer;
            _worldData = worldData;
        }

        public void Load()
        {
            _enemyPrefabOrk = (GameObject) Resources.Load(AssetPath.EnemyOrk);
            //_enemyPrefabSmartOrk = (GameObject) Resources.Load(AssetPath.EnemyOrk);
            
            _projectilePrefab = Resources.Load<Projectile>(AssetPath.HeroWeaponsProjectile);
            
            _warriorPrefab1 = (GameObject) Resources.Load(AssetPath.WarriorPrefab1);

            _heroCameraPrefab = (GameObject) Resources.Load(AssetPath.HeroCameraPath);


        }

        public void CreateEnemy(EnemyType enemyType, Vector2 at)
        {
            GameObject enemyPrefab = _enemyPrefabOrk;

            EnemyAttacker enemyInstance =
                _diContainer.InstantiatePrefab(enemyPrefab, at, Quaternion.identity, null).GetComponent<EnemyAttacker>();
            
            
            ActiveEnemies.Add(enemyInstance);
            enemyInstance.GetComponent<EnemyDeath>().OnDeathEvent += RemoveEnemyFromList;

            IHealth health = enemyInstance.GetComponent<IHealth>();
            health.Max = 10;
            health.Current = 10;
            
            enemyInstance.GetComponent<ActorUI>().Construct(health);
            
        }

        private void RemoveEnemyFromList(EnemyDeath destroyedEnemy, GameObject destroyer)
        {
            destroyedEnemy.OnDeathEvent -= RemoveEnemyFromList;

            ActiveEnemies.Remove(destroyedEnemy.GetComponent<EnemyAttacker>());
            Debug.Log($"Active enemies: {ActiveEnemies.Count}" );
        }

        public List<EnemyAttacker> GetActiveEnemiesList()
        {
            return ActiveEnemies;
        }

        public void CreateProjectile(GameObject attacker, Vector2 at, Transform directionTo, float projectileVelocity, Attack attack)
        {
            Projectile projectileInstance = _diContainer
                .InstantiatePrefabForComponent<Projectile>(_projectilePrefab, at, Quaternion.identity, null);
            
            projectileInstance.Launch(attacker, attack);

            Vector2 direction = ((Vector2) directionTo.position - at).normalized;

            projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileVelocity;
        }

        public GameObject CreateHero(Vector2 at)
        {
            GameObject heroPrefab = (GameObject) Resources.Load(AssetPath.Hero);


            _heroMove = _diContainer
                .InstantiatePrefabForComponent<HeroMove>(heroPrefab, at, Quaternion.identity, null);

            _heroMove.GetComponent<HeroHealth>().Max = 10; //TODO in static data

            _diContainer
                .Bind<HeroMove>()
                .FromInstance(_heroMove)
                .AsSingle();

            return _heroMove.gameObject;
        }

        public void CreateHud()
        {
            GameObject hudPrefab = (GameObject) Resources.Load(AssetPath.UIHud);
            
            _hudInstance = _diContainer
                .InstantiatePrefab(hudPrefab);
            
            _hudInstance.GetComponentInChildren<ActorUI>().Construct(_heroMove.GetComponent<IHealth>());
            _hudInstance.GetComponentInChildren<LootCounter>().Construct(_worldData);
        }

        public EnemySpawner CreateEnemySpawner()
        {
            EnemySpawner enemyFactoryPrefab = Resources.Load<EnemySpawner>(AssetPath.EnemySpawner);
            
            EnemySpawner enemySpawnerInstance = _diContainer
                .InstantiatePrefab(enemyFactoryPrefab).GetComponent<EnemySpawner>();
            
            enemySpawnerInstance.StartSpawnEnemy();
            
            return enemySpawnerInstance;

        }

        public GameObject CreateWarrior(Transform transform)
        {
            GameObject warriorPrefab = _warriorPrefab1;

            GameObject warriorInstance =
                _diContainer.InstantiatePrefab(warriorPrefab, (Vector2) transform.position + Vector2.down, Quaternion.identity, null);
            
            IHealth health = warriorInstance.GetComponent<IHealth>();
            health.Max = 5;
            health.Current = 5;
            
            warriorInstance.GetComponent<ActorUI>().Construct(health);  //TODO in static data

            return warriorInstance;
        }

        public LootItem CreateLoot()
        {
            GameObject prefab = Resources.Load<GameObject>(AssetPath.Loot);
            
            LootItem lootItem = _diContainer.InstantiatePrefab(prefab).GetComponent<LootItem>();
            lootItem.Construct(_worldData);
            return lootItem;
        }

        public void CreateHeroCamera(Transform heroTransform)
        {
            CinemachineVirtualCamera heroCamera = _diContainer.InstantiatePrefab(_heroCameraPrefab).GetComponent<CinemachineVirtualCamera>();
            heroCamera.Follow = heroTransform;
        }
    }
}
