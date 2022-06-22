using CodeBase.UI.Windows;

namespace CodeBase.Infrastructure.Upgrades
{
    public interface IUpgradesService
    {
        void TryBuyUpgrade(UpgradeButtonType upgradeButtonType);
    }
}