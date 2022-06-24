using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Loot
{
    public class LootItem : MonoBehaviour
    {
        private Loot _loot;
        private bool _picked;
        
        private WorldData _worldData;

        [Inject]
        public void Construct(WorldData worldData) => 
            _worldData = worldData;


        public void Initialize(Loot loot) => 
            _loot = loot;

        private void OnTriggerEnter2D(Collider2D col) => 
            Pickup();

        private void Pickup()
        {
            if(_picked)
                return;
            
            _picked = true;

            UpdateWorldsData();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldsData() => 
            _worldData.LootData.Collect(_loot);

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(0.01f);
            Destroy(gameObject);
        }
    }
}