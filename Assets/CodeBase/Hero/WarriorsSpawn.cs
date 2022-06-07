using System;
using CodeBase.Infrastracture;
using CodeBase.Logic.Loot;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class WarriorsSpawn : MonoBehaviour
    {
        private IGameFactory _gameFactory;
        private WorldData _worldData;

        [Inject]
        private void Construct(IGameFactory gameFactory, WorldData worldData)
        {
            _gameFactory = gameFactory;
            _worldData = worldData;
        }

        private void Update()
        {
            TryCreateWarrior();
        }

        private void TryCreateWarrior()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CreateWarrior();
            }
        }

        private void CreateWarrior()
        {
            if (_worldData.LootData.Take(LootType.MONEY, 5))
            {
                var warrior = _gameFactory.CreateWarrior(transform);
            }
        }
    }
}