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

        public override void InstallBindings()
        {
            BindInstallerInterfaces();

            //BindHero();

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
            Debug.Log("BindHero");
            
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
        }
    }
}