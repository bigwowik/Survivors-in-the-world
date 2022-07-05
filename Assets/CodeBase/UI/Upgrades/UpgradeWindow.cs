using System;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Infrastructure.Upgrades;
using CodeBase.Logic.Loot;
using CodeBase.UI.Windows;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Upgrades
{
    public class UpgradeWindow : WindowBase
    {
        public UpgradeButton[] UpgradeButtons;
        
        public TextMeshProUGUI PriceText;
        
        private WorldData _worldData;
        private IDifficultyService _difficultyService;
        private int _upgradePrice;

        [Inject]
        private void Construct(WorldData worldData, IDifficultyService difficultyService)
        {
            _worldData = worldData;
            _difficultyService = difficultyService;
        }

        protected override void Initialize()
        {
            UpdatePrice();
        }

        protected override void SubscribeUpdate()
        {
            _difficultyService.UpgradeWasCompleted += UpdatePrice;
            _worldData.LootData.Changed += UpdatePrice;
        }

        protected override void CleanUp()
        {
            _difficultyService.UpgradeWasCompleted -= UpdatePrice;
            _worldData.LootData.Changed -= UpdatePrice;
        }

        
        private void UpdatePrice()
        {
            _upgradePrice = _difficultyService.GetUpgradePrice();
            SetPrice(_upgradePrice);
            SetUpgradeAvailable(_worldData.LootData.Collected[LootType.MONEY] >= _upgradePrice);
        }

        private void SetPrice(int price) => 
            PriceText.text = price + "";

        private void SetUpgradeAvailable(bool upgradeAvailable)
        {
            foreach (var upgradeButton in UpgradeButtons) 
                upgradeButton.TrySetAvailable(upgradeAvailable);
        }
    }
}