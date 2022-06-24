using System.Collections.Generic;
using Cinemachine;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastructure.AssetsManagment;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using CodeBase.Map;
using CodeBase.Stats;
using CodeBase.UI.Elements;
using CodeBase.UI.Upgrades;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const int StartHeroHpValue = 10;
        private const int WarriorStartHp = 5;

        private readonly DiContainer _diContainer;
        private readonly WorldData _worldData;
        private readonly IDifficultyService _difficultyService;


        private List<EnemyAttacker> ActiveEnemies = new List<EnemyAttacker>();
        private List<WarriorDeath> ActiveWarriors = new List<WarriorDeath>();

        private HeroMove _heroMove;
        private GameObject _hudInstance;
        private Projectile _projectilePrefab;
        private GameObject _warriorPrefab1;
        private GameObject _heroCameraPrefab;
        private GameObject _enemyPrefabOrk;


        public GameObject Hero { get; set; }

        public GameFactory(DiContainer diContainer, WorldData worldData, IDifficultyService difficultyService)
        {
            _diContainer = diContainer;
            _worldData = worldData;
            _difficultyService = difficultyService;
        }

        public void Load()
        {
            _enemyPrefabOrk = (GameObject) Resources.Load(AssetPath.EnemyOrk);
            _projectilePrefab = Resources.Load<Projectile>(AssetPath.HeroWeaponsProjectile);
            _warriorPrefab1 = (GameObject) Resources.Load(AssetPath.WarriorPrefab);
            _heroCameraPrefab = (GameObject) Resources.Load(AssetPath.HeroCameraPath);
        }

        public GameObject CreateTile(GameObject getRandomElement, Vector2 at, Transform parent) => 
            _diContainer.InstantiatePrefab(getRandomElement, at, Quaternion.identity, parent);

        public void CreateEnemy(EnemyType enemyType, Vector2 at)
        {
            GameObject enemyPrefab = _enemyPrefabOrk;

            EnemyAttacker enemyInstance =
                _diContainer.InstantiatePrefab(enemyPrefab, at, Quaternion.identity, null).GetComponent<EnemyAttacker>();
            
            ActiveEnemies.Add(enemyInstance);
            enemyInstance.GetComponent<EnemyDeath>().OnDeathEvent += RemoveEnemyFromList;

            IHealth health = enemyInstance.GetComponent<IHealth>();

            health.Max = (int)_difficultyService.EnemyMaxHpValue();
            //health.Current = (int)_difficultyService.EnemyMaxHpValue();
            
            _difficultyService.EnemyIncreaseCounter();
            
            enemyInstance.GetComponent<ActorUI>().Construct(health);
            
        }

        private void RemoveEnemyFromList(EnemyDeath destroyedEnemy, GameObject destroyer)
        {
            destroyedEnemy.OnDeathEvent -= RemoveEnemyFromList;

            ActiveEnemies.Remove(destroyedEnemy.GetComponent<EnemyAttacker>());
            Debug.Log($"Active enemies: {ActiveEnemies.Count}" );
        }

        public List<EnemyAttacker> GetActiveEnemiesList() => 
            ActiveEnemies;

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

            _heroMove.GetComponent<IHealth>().Max = StartHeroHpValue; //TODO in static data

            _diContainer
                .Bind<HeroMove>()
                .FromInstance(_heroMove)
                .AsTransient();

            Hero = _heroMove.gameObject;

            return Hero;
        }

        public void CreateHud()
        {
            GameObject hudPrefab = (GameObject) Resources.Load(AssetPath.UIHud);
            
            _hudInstance = _diContainer
                .InstantiatePrefab(hudPrefab);
            
            _hudInstance.GetComponentInChildren<ActorUI>().Construct(_heroMove.GetComponent<IHealth>());
            _hudInstance.GetComponentInChildren<LootCounter>().Construct(_worldData);

            var upgradeWindow = _hudInstance.GetComponentInChildren<UpgradeWindow>();
            
            _diContainer
                .Bind<UpgradeWindow>()
                .FromInstance(upgradeWindow)
                .AsSingle();
            
            _hudInstance.GetComponentInChildren<DamageIndicator>().Init(_heroMove.GetComponent<PlayerWeaponHandler>().Weapon);
            
        }

        public void CreateEnemySpawner()
        {
            EnemySpawner enemyFactoryPrefab = Resources.Load<EnemySpawner>(AssetPath.EnemySpawner);
            
            EnemySpawner enemySpawnerInstance = _diContainer
                .InstantiatePrefab(enemyFactoryPrefab).GetComponent<EnemySpawner>();
            
            enemySpawnerInstance.StartSpawnEnemy();

        }



        public void CreateWarrior()
        {
            GameObject warriorPrefab = _warriorPrefab1;

            GameObject warriorInstance =
                _diContainer.InstantiatePrefab(warriorPrefab, (Vector2) _heroMove.transform.position + Vector2.down, Quaternion.identity, null);
            
            IHealth health = warriorInstance.GetComponent<IHealth>();
            health.Max = WarriorStartHp;
            health.Current = WarriorStartHp;//TODO in static data
            
            warriorInstance.GetComponent<ActorUI>().Construct(health);  

            warriorInstance.GetComponent<WarriorWeaponHandler>().Weapon =
                _heroMove.GetComponent<PlayerWeaponHandler>().Weapon;

            
            var warriorDeath = warriorInstance.GetComponent<WarriorDeath>();
            ActiveWarriors.Add(warriorDeath);
            warriorDeath.OnDeathEvent += OnWarriorDeath;
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
        
        public void CreateMapGenerator()
        {
            MapGenerator prefab = Resources.Load<MapGenerator>(AssetPath.MapGeneratorPath);
            
            var instance = _diContainer
                .InstantiatePrefab(prefab).GetComponent<MapGenerator>();
            
            instance.Init();

        }

        public void Reset()
        {
            foreach (var enemy in ActiveEnemies)
            {
                enemy.GetComponent<EnemyDeath>().OnDeathEvent -= RemoveEnemyFromList;

                GameObject.Destroy(enemy.gameObject);
            }
            ActiveEnemies.Clear();
            
            

            foreach (var warrior in ActiveWarriors)
            {
                warrior.GetComponent<WarriorDeath>().OnDeathEvent -= OnWarriorDeath;
                
                GameObject.Destroy(warrior.gameObject);
            }
            ActiveWarriors.Clear();

            
        }

        private void OnWarriorDeath(WarriorDeath warrior, GameObject destroyer)
        {
            warrior.GetComponent<WarriorDeath>().OnDeathEvent -= OnWarriorDeath;
            ActiveWarriors.Remove(warrior);
        }
    }
}
