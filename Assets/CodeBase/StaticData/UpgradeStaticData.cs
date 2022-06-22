using UnityEngine;

namespace CodeBase.StaticData
{
    
    [CreateAssetMenu(fileName = "UpgradeStaticData", menuName = "Data/UpgradeStaticData", order = 1)]

    public class UpgradeStaticData : ScriptableObject
    {
        public int StartUpgradePrice;
        public float UpgradePriceIncreaser;
        public float DamageUpgradeIncreaser = 1;
        public float HpIncreaserValue = 1;
    }
}