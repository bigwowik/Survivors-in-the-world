using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public interface IEnemyFactory
    {
        void Load();
        void Create(EnemyType enemyType, Vector2 at);
    }
}