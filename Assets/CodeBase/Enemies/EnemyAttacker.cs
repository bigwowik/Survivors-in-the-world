using System;
using System.ComponentModel;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Stats;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyAttacker : MonoBehaviour
    {
        public Attack Attack;

        private IDamagable _targetHealth;
        private float _lastAttackTime;


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamagable targetHealth) && targetHealth is IPlayerTeam)
            {
                _targetHealth = targetHealth;
                TryAttack();
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamagable targetHealth) && targetHealth is IPlayerTeam && _targetHealth == targetHealth)
            {
                _targetHealth = null;
            }
                
        }

        private void Update()
        {
            if (_targetHealth == null)
                return;
            
            TryAttack();
        }
        private void TryAttack()
        {
            if (Time.time >= _lastAttackTime + Attack.Cooldown)
            {
                _lastAttackTime = Time.time;
                _targetHealth.TryTakeDamage(gameObject, Attack);
            }
        }
    }
}