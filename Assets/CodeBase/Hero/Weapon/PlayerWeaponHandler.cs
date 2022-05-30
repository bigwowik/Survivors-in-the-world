using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemies;
using CodeBase.Infrastracture;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Hero.Weapon
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        public WeaponBase Weapon;
        public float PlayerAttackRadius = 15f;
        
        private IGameFactory _gameFactory;
        private float timer = 0f;
        private List<EnemyAttacker> _activeEnemiesList;


        [Inject]
        private void Construct(IGameFactory gameFactory) => _gameFactory = gameFactory;


        public void Update()
        {
            TimerUpdateWithAction(Weapon.Attack.Cooldown,
                TryFindEnemy(out var enemy),
                () =>  Weapon.Shoot(gameObject, _gameFactory, this, enemy.transform));
        }

        private bool TryFindEnemy(out EnemyAttacker enemy)
        {
            enemy = null;
            
            GetActiveEnemiesList();

            if (_activeEnemiesList.Count == 0)
                return false;
            
            return TryFindNearEnemyFromList(ref enemy);
        }

        private void GetActiveEnemiesList()
        {
            _activeEnemiesList ??= _gameFactory.GetActiveEnemiesList();
        }

        private bool TryFindNearEnemyFromList(ref EnemyAttacker enemy)
        {
            if (Helper.GetArrayOfTypeByGameObjects<Transform, EnemyAttacker>(_activeEnemiesList, out var transformList))
            {
                var nearEnemy = Helper.GetNearTransform(transform, transformList.ToArray(), PlayerAttackRadius);
                enemy = nearEnemy.GetComponent<EnemyAttacker>();

                return true;
            }
            else
                return false;
        }

        private void TimerUpdateWithAction(float timerCooldown, bool check, Action timerAction)
        {
            if (timer > timerCooldown)
            {
                if (check)
                {
                    timer = 0;
                    timerAction?.Invoke();
                }
            }
            else
                timer += Time.deltaTime;
        }
    }
}