using System;
using UnityEngine;

namespace CodeBase.Stats
{
    public class EnemyDeath : MonoBehaviour, IDestructable
    {
        public event Action<EnemyDeath, GameObject> OnDeathEvent; 
        public void OnDestruction(GameObject destroyer)
        {
            OnDeathEvent?.Invoke(this, destroyer);
            Destroy(gameObject);
        }
    }
}