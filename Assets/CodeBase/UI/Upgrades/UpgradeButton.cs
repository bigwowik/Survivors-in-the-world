using System;
using CodeBase.Infrastructure.Difficulty;
using CodeBase.Infrastructure.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        public UpgradeButtonType UpgradeButtonType;
        
        private Button _button;
        private IUpgradesService _upgradesService;

        [Inject]
        private void Construct(IUpgradesService upgradesService) => 
            _upgradesService = upgradesService;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => 
            _upgradesService.TryBuyUpgrade(UpgradeButtonType);

        public void TrySetAvailable(bool upgradeAvailable)
        {
            if (_button.interactable != upgradeAvailable)
                _button.interactable = upgradeAvailable;
        }
    }
}