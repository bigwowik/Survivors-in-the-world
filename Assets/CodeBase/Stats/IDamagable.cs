using CodeBase.Hero.Weapon;
using UnityEngine;

namespace CodeBase.Stats
{
    public interface IDamagable
    {
        void TryTakeDamage(GameObject attacker, Attack attack);
    }
}