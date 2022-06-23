using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Loot;
using CodeBase.StaticData;
using CodeBase.UI.Upgrades;
using UnityEngine;

namespace CodeBase.Infrastructure.Upgrades
{
    public class UpgradesService : IUpgradesService
    {
        private readonly IGameFactory _gameFactory;
        private readonly WorldData _worldData;
        private readonly IDifficultyService _difficultyService;
        private readonly IStaticDataService _staticDataService;

        public UpgradesService(IGameFactory gameFactory, WorldData worldData, IDifficultyService difficultyService, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _worldData = worldData;
            _difficultyService = difficultyService;
            _staticDataService = staticDataService;
        }

        public void TryBuyUpgrade(UpgradeButtonType upgradeButtonType)
        {
            var price = _difficultyService.GetUpgradePrice();
            if (!_worldData.LootData.Take(LootType.MONEY, price)) 
                return;
            
            switch (upgradeButtonType)
            {
                case UpgradeButtonType.BUY_SOLDIER:
                    CreateWarrior();
                    break;
                case UpgradeButtonType.DAMAGE_UPGRADE:
                    UpgradeDamage();
                    break;
                case UpgradeButtonType.BUY_HP:
                    BuyHp();
                    break;
            }
            
            _difficultyService.CompleteUpgrade();
        }

        private void CreateWarrior()
        {
            _gameFactory.CreateWarrior();
        }

        private void UpgradeDamage()
        {
            _gameFactory.Hero.GetComponent<PlayerWeaponHandler>().Weapon.Attack.AttackValue += _staticDataService.GetUpgradeStaticData().DamageUpgradeIncreaser;
            Debug.Log("Try Upgrade damage");
        }

        private void BuyHp()
        {
            _gameFactory.Hero.GetComponent<IHealth>().Give(_staticDataService.GetUpgradeStaticData().HpIncreaserValue);
            Debug.Log("Try buy hp");
        }
    }
}