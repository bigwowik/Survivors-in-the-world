using CodeBase.Hero.Weapon;
using UnityEngine;

namespace CodeBase.Stats
{
    [RequireComponent(typeof(EnemyDeath))]
    public class EnemyStats : MonoBehaviour, IDamagable
    {
        public int MaxHp = 10;
        public int CurrentHp;

        private void Start() => 
            CurrentHp = MaxHp;

        public void TryTakeDamage(GameObject attacker,Attack attack)
        {
            CurrentHp -= Mathf.RoundToInt(attack.AttackValue);
            Debug.Log($"{name} take damage {attack.AttackValue}. Now HP: {CurrentHp}");

            if (CurrentHp <= 0)
                Death(attacker);
        }

        private void Death(GameObject attacker)
        {
            foreach (IDestructable destructable in GetComponentsInChildren<IDestructable>())
                destructable.OnDestruction(attacker);
        }
    }
}