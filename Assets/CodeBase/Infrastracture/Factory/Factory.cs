using System;
using CodeBase.Hero;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastracture
{
    public class Factory : IFactory
    {
        private readonly DiContainer _diContainer;

        private const string Hero = "Hero/Hero";

        private const string EnemyOrc = "Enemies/Orc";
        private const string EnemySmartOrk = "Enemies/Smart Orc";


        private GameObject _enemyPrefabOrk;
        private GameObject _enemyPrefabSmartOrk;


        public Factory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void LoadEnemies()
        {
            _enemyPrefabOrk = (GameObject) Resources.Load(EnemyOrc);
            _enemyPrefabSmartOrk = (GameObject) Resources.Load(EnemySmartOrk);


        }

        public void Create(EnemyType enemyType, Vector2 at)
        {
            switch (enemyType)
            {
                case EnemyType.Ork:
                    _diContainer.InstantiatePrefab(_enemyPrefabOrk, at, Quaternion.identity, null);
                    break;
                case EnemyType.SmartOrk:
                    _diContainer.InstantiatePrefab(_enemyPrefabSmartOrk, at, Quaternion.identity, null);
                    break;
                default:
                    break;
            }
        }

        public void CreateHero(Vector2 at)
        {
            GameObject heroPrefab = (GameObject) Resources.Load(Hero);


            HeroMove heroMove = _diContainer
                .InstantiatePrefabForComponent<HeroMove>(heroPrefab, at, Quaternion.identity, null);

            _diContainer
                .Bind<HeroMove>()
                .FromInstance(heroMove)
                .AsSingle();
        }
    }
}
