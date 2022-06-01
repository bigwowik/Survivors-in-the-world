using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataLevelsPath = "StaticData/Levels";
        
        private Dictionary<string,LevelStaticData> _levels;
        
        public void LoadData()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData) 
                ? levelStaticData 
                : null;

    }

    public interface IStaticDataService
    {
        void LoadData();
        LevelStaticData ForLevel(string sceneKey);
    }
}