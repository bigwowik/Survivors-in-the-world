using System.Collections;
using CodeBase.Hero;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastracture
{
    public class LocationInstaller : MonoInstaller, IInitializable
    {
        public Transform StartPoint;
        public GameObject HeroPrefab;
        public EnemyMarker[] EnemyMarkers;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();

            BindHero();

            BindEnemyFactory();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IEnemyFactory>()
                .To<EnemyFactory>()
                .AsSingle();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindHero()
        {
            HeroMove heroMove = Container
                .InstantiatePrefabForComponent<HeroMove>(HeroPrefab, StartPoint.position, Quaternion.identity, null);

            Container
                .Bind<HeroMove>()
                .FromInstance(heroMove)
                .AsSingle();
        }

        public void Initialize()
        {
            Debug.Log("Initialize");
            
            // IEnemyFactory enemyFactory = Container.Resolve<IEnemyFactory>();
            //
            // enemyFactory.Load();
            //
            // foreach (var enemyMarker in EnemyMarkers)
            // {
            //     enemyFactory.Create(enemyMarker.EnemyType, enemyMarker.transform.position);
            // }
            
        }
    }
}