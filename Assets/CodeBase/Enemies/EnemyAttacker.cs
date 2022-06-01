using System;
using System.ComponentModel;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        public Attack Attack;

        private HeroHealth _currentHeroHealth;
        private float _lastAttackTime;


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out HeroHealth heroHealth))
            {
                _currentHeroHealth = heroHealth;
                TryAttack();
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.collider.TryGetComponent(out HeroHealth heroHealth)) 
                _currentHeroHealth = null;
        }

        private void Update()
        {
            if (_currentHeroHealth == null)
                return;
            
            TryAttack();
        }
        private void TryAttack()
        {
            if (Time.time >= _lastAttackTime + Attack.Cooldown)
            {
                _lastAttackTime = Time.time;
                _currentHeroHealth.TryTakeDamage(gameObject, Attack);
            }
        }
    }
}