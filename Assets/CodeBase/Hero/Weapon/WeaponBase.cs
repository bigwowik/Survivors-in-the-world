﻿using CodeBase.Helpers;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Hero.Weapon
{
    [CreateAssetMenu(menuName = "Weapon/CreateEnemy WeaponBase", fileName = "WeaponBase", order = 0)]
    public class WeaponBase : ScriptableObject
    {

        public float ProjectileVelocity = 1f;
        public Attack Attack;

        public virtual void Shoot(GameObject attacker, IGameFactory gameFactory, WeaponHandler weaponHandler, Transform enemy)
        {
            gameFactory.CreateProjectile(attacker, weaponHandler.transform.position, enemy, ProjectileVelocity, Attack);
            
            Helper.DrawCross(enemy.transform.position);
            //Debug.Log($"WeaponBase {name}. Shoot to enemy: {enemy}.");
        }
    }
}