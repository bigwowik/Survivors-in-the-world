using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public interface IGameFactory
    {
        void Load();
        void CreateEnemy(EnemyType enemyType, Vector2 at);
        void CreateHero(Vector2 at);
        List<EnemyAttacker> GetActiveEnemiesList();
        void CreateProjectile(GameObject attacker, Vector2 at, Transform directionTo, float projectileVelocity, Attack attack); //TODO only creating
        void CreateHud();
        EnemySpawner CreateEnemySpawner();
        GameObject CreateWarrior(Transform transform);
        LootItem CreateLoot();
    }
}