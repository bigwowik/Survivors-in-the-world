using UnityEngine.SceneManagement;

namespace CodeBase.Infrastracture.States
{
    public class LoadLevelState : IState
    {
        private const string SceneName = "Main";

        private readonly IGameStateMachine _gameStateMachine;

        public LoadLevelState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            SceneManager.LoadSceneAsync(SceneName);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }
    }
}