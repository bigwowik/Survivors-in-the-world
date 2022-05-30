using CodeBase.Infrastracture;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Hero.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/Create WeaponBase", fileName = "WeaponBase", order = 0)]
    public class WeaponBase : ScriptableObject
    {

        public float ProjectileVelocity = 1f;
        public Attack Attack;
        
        private IGameFactory _gameFactory;


        [Inject]
        private void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public virtual void Shoot(GameObject attacker, IGameFactory gameFactory, PlayerWeaponHandler weaponHandler, Transform enemy)
        {
            gameFactory.CreateProjectile(attacker, weaponHandler.transform.position, enemy, ProjectileVelocity, Attack);
            
            Helper.DrawCross(enemy.transform.position);
            Debug.Log($"WeaponBase {name}. Shoot to enemy: {enemy}.");
        }
    }
}