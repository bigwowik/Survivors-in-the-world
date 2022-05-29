using System;
using CodeBase.Infrastracture.States;
using UnityEngine;

namespace CodeBase.Infrastracture
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(new GameStateMachine());
            _game.gameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
            
        }
    }
}