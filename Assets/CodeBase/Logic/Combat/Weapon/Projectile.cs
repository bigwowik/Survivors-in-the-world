using System;
using CodeBase.Stats;
using UnityEngine;

namespace CodeBase.Hero.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Attack _attack;
        private GameObject _attacker;

        public void Launch(GameObject attacker, Attack attack)
        {
            _attacker = attacker;
            _attack = attack;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out IDamagable damagable)) 
                damagable.TryTakeDamage(_attacker, _attack);
            
            Destroy(gameObject);
        }
        
    }
}