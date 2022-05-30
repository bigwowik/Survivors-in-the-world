using System;
using CodeBase.Infrastracture.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastracture
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        [Inject]
        private void Construct(Game game)
        {
            _game = game;
        }
        
        private void Awake()
        {
            _game.gameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
            
        }
    }
}