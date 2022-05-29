using System;
using CodeBase.Hero;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastracture
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IInstantiator _instantiator;
        private const string EnemyOrc = "Enemies/Orc";
        private const string EnemySmartOrk = "Enemies/Smart Orc";
        
        
        private GameObject _enemyPrefabOrk;
        private GameObject _enemyPrefabSmartOrk;


        public EnemyFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Load()
        {
            _enemyPrefabOrk = (GameObject) Resources.Load(EnemyOrc);
            _enemyPrefabSmartOrk = (GameObject) Resources.Load(EnemySmartOrk);
        }

        public void Create(EnemyType enemyType, Vector2 at)
        {
            switch (enemyType)
            {
                case EnemyType.Ork:
                    _instantiator.InstantiatePrefab(_enemyPrefabOrk, at, Quaternion.identity, null);
                    break;
                case EnemyType.SmartOrk:
                    _instantiator.InstantiatePrefab(_enemyPrefabSmartOrk, at, Quaternion.identity, null);
                    break;
                default:
                    break;
            }
        }
    }
}