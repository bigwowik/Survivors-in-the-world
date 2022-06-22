namespace CodeBase.StaticData
{
    public interface IStaticDataService
    {
        void LoadData();
        LevelStaticData ForLevel(string sceneKey);
        UpgradeStaticData GetUpgradeStaticData();
    }
}