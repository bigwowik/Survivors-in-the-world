using System.Collections;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Infrastracture.Difficulty;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastracture
{
    public class LocationInstaller : MonoInstaller, IInitializable
    {
        public Transform StartPoint;
        public GameObject HeroPrefab;

        public override void InstallBindings()
        {
            Debug.Log("LocationInstaller - InstallBindings");
            BindInstallerInterfaces();

            //BindHero();

            //BindDifficultyService();
            
            BindEnemySpawner();
        }

        

        private void BindEnemySpawner()
        {
            Container
                .Bind<EnemySpawner>()
                .AsSingle();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public void Initialize()
        {
            Debug.Log("Initialize");
        }
    }
}