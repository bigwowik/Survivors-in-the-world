using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public interface IFactory
    {
        void LoadEnemies();
        void Create(EnemyType enemyType, Vector2 at);
        void CreateHero(Vector2 at);
    }
}