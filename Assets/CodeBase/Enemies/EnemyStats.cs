using System;
using CodeBase.Hero.Weapon;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Stats
{
    [RequireComponent(typeof(EnemyDeath))]
    public class EnemyStats : MonoBehaviour, IDamagable, IHealth
    {
        public event Action HealthChanged;
        public float Current { get; set; }
        public float Max { get; set; }
        public void TakeDamage(float damage)
        {
        }

        private void Start() => 
            Current = Max;

        public void TryTakeDamage(GameObject attacker,Attack attack)
        {
            Current -= Mathf.RoundToInt(attack.AttackValue);
            HealthChanged?.Invoke();
            
            Debug.Log($"{name} take damage {attack.AttackValue}. Now HP: {Current}");
            
            

            if (Current <= 0)
                Death(attacker);
        }

        private void Death(GameObject attacker)
        {
            foreach (IDestructable destructable in GetComponentsInChildren<IDestructable>())
                destructable.OnDestruction(attacker);
        }
    }
}