using System.Collections.Generic;
using System.Linq;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Hero.Weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        public WeaponBase WeaponTemplate;
        public WeaponBase Weapon;
        public float PlayerAttackRadius = 15f;
        
        private IGameFactory _gameFactory;
        
        private List<EnemyAttacker> _activeEnemiesList;
        private TimerUpdater _timerUpdater;


        [Inject]
        private void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;


        private void Start()
        {
            Init();
            _timerUpdater = new TimerUpdater();
            
        }

        protected virtual void Init()
        {
        }

        public void Update()
        {
            _timerUpdater.TimerUpdateWithAction(Weapon.Attack.Cooldown,
                TryFindEnemy(out var enemy),
                () =>  Weapon.Shoot(gameObject, _gameFactory, this, enemy.transform), Time.deltaTime);
        }

        private bool TryFindEnemy(out EnemyAttacker enemy)
        {
            enemy = null;
            
            GetActiveEnemiesList();

            if (_activeEnemiesList.Count == 0)
                return false;
            
            return TryFindNearEnemyFromList(ref enemy);
        }

        private void GetActiveEnemiesList() => 
            _activeEnemiesList ??= _gameFactory.GetActiveEnemiesList();

        private bool TryFindNearEnemyFromList(ref EnemyAttacker enemy)
        {
            if (Helper.GetArrayOfTypeByGameObjects<Transform, EnemyAttacker>(_activeEnemiesList, out var transformList))
            {
                var nearEnemy = Helper.GetNearTransform(transform, transformList.ToArray(), PlayerAttackRadius);
                
                if(nearEnemy!= null)
                    enemy = nearEnemy.GetComponent<EnemyAttacker>();
                else
                    return false;

                return true;
            }
            else
                return false;
        }

        
    }
}