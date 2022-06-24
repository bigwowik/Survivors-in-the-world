using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Loot
{

    [Serializable]
    public class LootData
    {
        public Dictionary<LootType, int> Collected = new Dictionary<LootType, int>();
        public Action Changed;

        public LootData()
        {
            Collected[LootType.MONEY] = 0;
            Collected[LootType.CRYSTAL] = 0;
        }

        
        public void Collect(Loot loot)
        {
            Collected[loot.LootType] += loot.Value;
            Changed?.Invoke();
            
            Debug.Log($"loot.LootType - {loot.LootType} . New value- {Collected[loot.LootType]}");

        }

        public void Add(LootType lootType, int value)
        {
            Collected[lootType] += value;
            Changed?.Invoke();
            
            Debug.Log($"loot.LootType - {lootType} . New value- {Collected[lootType]}");

        }

        public bool Take(LootType lootType, int value)
        {
            if (Collected[lootType] >= value)
            {
                Collected[lootType] -= value;
                Changed?.Invoke();
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            Collected[LootType.MONEY] = 0;
            Collected[LootType.CRYSTAL] = 0;
            Changed?.Invoke();
        }
    }

}