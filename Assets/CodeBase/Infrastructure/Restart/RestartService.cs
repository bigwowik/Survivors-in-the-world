using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Restart
{
    public class RestartService : IRestartService
    {
        private readonly IGameStateMachine _gameStateMachine;
        public RestartService(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void RestartGame() => 
            _gameStateMachine.Enter<LoadLevelState>();
    }
}