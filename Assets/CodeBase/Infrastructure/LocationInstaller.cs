using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Upgrades;
using CodeBase.Map;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LocationInstaller : MonoInstaller, IInitializable
    {
        private IGameFactory _gameFactory;
        private IStaticDataService _staticDataService;

        public override void InstallBindings()
        {
            Debug.Log("LocationInstaller - InstallBindings");
            BindInstallerInterfaces();

            LoadResources();
            
            Bindings();

        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        [Inject]
        private void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        void Bindings()
        {
            CreateHeroAndCamera();
            CreateHud();
            CreateSpawner();
            CreateMapGenerator();
        }

        private void LoadResources()
        {
            _staticDataService.LoadData();
            _gameFactory.Load();
        }

        private void CreateMapGenerator()
        {
            _gameFactory.CreateMapGenerator();
        }

        private void CreateSpawner()
        {
            _gameFactory.CreateEnemySpawner();
        }

        private void CreateHud() =>
            _gameFactory.CreateHud();

        private void CreateHeroAndCamera()
        {
            Vector2 heroStartPoint = Vector2.zero;
            var hero = _gameFactory.CreateHero(heroStartPoint);
            _gameFactory.CreateHeroCamera(hero.transform);
        }


        public void Initialize()
        {
            Debug.Log("Initialize");

            
        }
    }
}