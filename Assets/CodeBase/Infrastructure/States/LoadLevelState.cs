using CodeBase.Infrastructure.Loading;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        

        private readonly IGameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter(string nextLevelName)
        {
            _sceneLoader.Load(nextLevelName, OnLoaded);
        }

        public void Exit()
        {
        }

        void OnLoaded()
        {
            Debug.Log("LoadLevelState - On loaded");
            
            _gameStateMachine.Enter<GameLoopState>();
        }

    }
}