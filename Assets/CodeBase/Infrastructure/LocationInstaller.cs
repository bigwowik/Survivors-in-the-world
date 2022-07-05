using CodeBase.Enemies;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Upgrades;
using CodeBase.Logic.Loot;
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
            BindInstallerInterfaces();
            
            BindEnemyFactory();
            BindUpdateService();
            BindStaticDataService();
            BindWorldData();
            BindDifficultyService();
        }

        public void Initialize()
        {
            _gameFactory = Container.Resolve<IGameFactory>();
            _staticDataService = Container.Resolve<IStaticDataService>();
            
            Creating();
        }

        private void BindDifficultyService()
        {
            Container
                .Bind<IDifficultyService>()
                .To<DifficultyService>()
                .AsSingle();

        }

        private void BindWorldData()
        {
            Container
                .Bind<WorldData>()
                .AsSingle();
        }

        private void BindStaticDataService()
        {
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle();
        }


        void Creating()
        {
            LoadResources();
            
            CreateHeroAndCamera();
            CreateHud();
            CreateSpawner();
            CreateMapGenerator();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
        }
        
        private void BindUpdateService()
        {
            Container
                .Bind<IUpgradesService>()
                .To<UpgradesService>()
                .AsSingle();
        }

        private void LoadResources()
        {
            _staticDataService.LoadData();
            _gameFactory.Load();
        }

        private void CreateMapGenerator() => 
            _gameFactory.CreateMapGenerator();

        private void CreateSpawner() => 
            _gameFactory.CreateEnemySpawner();

        private void CreateHud() =>
            _gameFactory.CreateHud();

        private void CreateHeroAndCamera()
        {
            Vector2 heroStartPoint = Vector2.zero;
            var hero = _gameFactory.CreateHero(heroStartPoint);
            _gameFactory.CreateHeroCamera(hero.transform);
        }
    }
}