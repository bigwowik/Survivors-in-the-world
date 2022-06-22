using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string UpgradeStaticDataPath = "StaticData/UpgradeStaticData";
        
        private Dictionary<string,LevelStaticData> _levels;

        private UpgradeStaticData _upgradeStaticData;
        
        public void LoadData()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
            
            _upgradeStaticData = Resources.Load<UpgradeStaticData>(UpgradeStaticDataPath);
        }

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData) 
                ? levelStaticData 
                : null;

        public UpgradeStaticData GetUpgradeStaticData() => 
            _upgradeStaticData;
    }
}