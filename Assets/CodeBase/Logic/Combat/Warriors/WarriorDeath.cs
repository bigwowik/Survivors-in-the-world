using System;
using CodeBase.Stats;
using UnityEngine;

namespace CodeBase.Hero.Weapon
{
    public class WarriorDeath : MonoBehaviour, IDestructable
    {
        public event Action<WarriorDeath, GameObject> OnDeathEvent;
        public void OnDestruction(GameObject destroyer)
        {
            OnDeathEvent?.Invoke(this, destroyer);
            Destroy(gameObject);
        }


    }
}