using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Hero;
using CodeBase.Hero.Weapon;
using CodeBase.Logic.Loot;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        void Load();
        void CreateEnemy(EnemyType enemyType, Vector2 at);
        GameObject CreateHero(Vector2 at);
        List<EnemyAttacker> GetActiveEnemiesList();
        void CreateProjectile(GameObject attacker, Vector2 at, Transform directionTo, float projectileVelocity, Attack attack); //TODO only creating
        void CreateHud();
        void CreateEnemySpawner();
        void CreateWarrior();
        LootItem CreateLoot();
        void CreateHeroCamera(Transform heroTransform);
        GameObject Hero { get; set; }
        GameObject CreateTile(GameObject getRandomElement, Vector2 pointCenter, Transform parent);
        void CreateMapGenerator();
        void Reset();
    }
}