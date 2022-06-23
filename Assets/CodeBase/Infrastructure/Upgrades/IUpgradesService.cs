using CodeBase.UI.Upgrades;

namespace CodeBase.Infrastructure.Upgrades
{
    public interface IUpgradesService
    {
        void TryBuyUpgrade(UpgradeButtonType upgradeButtonType);
    }
}