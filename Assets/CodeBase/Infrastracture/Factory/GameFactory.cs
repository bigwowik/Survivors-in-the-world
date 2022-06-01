using System;
using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastracture.AssetsManagment;
using CodeBase.Logic;
using CodeBase.Stats;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastracture
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;

        private GameObject _enemyPrefabOrk;
        private GameObject _enemyPrefabSmartOrk;

        public List<EnemyAttacker> ActiveEnemies = new List<EnemyAttacker>();
        private HeroMove heroMove;
        private GameObject hudInstance;


        public GameFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void LoadEnemies()
        {
            _enemyPrefabOrk = (GameObject) Resources.Load(AssetPath.EnemyOrc);
            _enemyPrefabSmartOrk = (GameObject) Resources.Load(AssetPath.EnemySmartOrk);


        }

        public void Create(EnemyType enemyType, Vector2 at)
        {
            EnemyAttacker enemyInstance = null;
            switch (enemyType)
            {
                case EnemyType.Ork:
                    enemyInstance = _diContainer.InstantiatePrefab(_enemyPrefabOrk, at, Quaternion.identity, null)
                        .GetComponent<EnemyAttacker>();
                    break;
                case EnemyType.SmartOrk:
                    enemyInstance = _diContainer.InstantiatePrefab(_enemyPrefabSmartOrk, at, Quaternion.identity, null)
                        .GetComponent<EnemyAttacker>();
                    break;
                default:
                    break;
            }
            
            ActiveEnemies.Add(enemyInstance);
            enemyInstance.GetComponent<EnemyDeath>().OnDeathEvent += RemoveEnemyFromList;
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
            Projectile projectilePrefab = Resources.Load<Projectile>(AssetPath.HeroWeaponsProjectile);
            
            Projectile projectileInstance = _diContainer
                .InstantiatePrefabForComponent<Projectile>(projectilePrefab, at, Quaternion.identity, null);
            
            projectileInstance.Launch(attacker, attack);

            Vector2 direction = ((Vector2) directionTo.position - at).normalized;

            projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileVelocity;
        }

        public void CreateHero(Vector2 at)
        {
            GameObject heroPrefab = (GameObject) Resources.Load(AssetPath.Hero);


            heroMove = _diContainer
                .InstantiatePrefabForComponent<HeroMove>(heroPrefab, at, Quaternion.identity, null);

            heroMove.GetComponent<HeroHealth>().Max = 10; //TODO in static data

            _diContainer
                .Bind<HeroMove>()
                .FromInstance(heroMove)
                .AsSingle();
        }

        public void CreateHud()
        {
            GameObject hudPrefab = (GameObject) Resources.Load(AssetPath.UIHud);
            
            hudInstance = _diContainer
                .InstantiatePrefab(hudPrefab);
            
            hudInstance.GetComponentInChildren<ActorUI>().Construct(heroMove.GetComponent<IHealth>());
        }

        public EnemySpawner CreateEnemySpawner()
        {
            EnemySpawner enemyFactoryPrefab = Resources.Load<EnemySpawner>(AssetPath.EnemySpawner);
            
            EnemySpawner enemySpawnerInstance = _diContainer
                .InstantiatePrefab(enemyFactoryPrefab).GetComponent<EnemySpawner>();
            
            enemySpawnerInstance.StartSpawnEnemy();
            
            return enemySpawnerInstance;

        }
        
        
    }
}
