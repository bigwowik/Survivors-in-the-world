using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Upgrades;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using CodeBase.Stats;
using Zenject;

namespace CodeBase.Infrastructure.Restart
{
    public class RestartService : IRestartService
    {
        private readonly IGameFactory _gameFactory;
        private readonly HeroEnabler _heroEnabler;
        private readonly WorldData _worldData;
        private readonly IDifficultyService _difficultyService;

        public RestartService(HeroMove heroMove, IGameFactory gameFactory, WorldData worldData, IDifficultyService difficultyService)
        {
            _heroEnabler = heroMove.GetComponent<HeroEnabler>();
            _gameFactory = gameFactory;
            _worldData = worldData;
            _difficultyService = difficultyService;
        }
        
        public void RestartGame()
        {
            _heroEnabler.EnablePlayer(true);
            _heroEnabler.GetComponent<PlayerWeaponHandler>().Reset();
            _heroEnabler.GetComponent<HeroHealth>().ResetHealth();
            
            _gameFactory.Reset();
            _worldData.LootData.Reset();
            _difficultyService.Reset();
        }
    }
}