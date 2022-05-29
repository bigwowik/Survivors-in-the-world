using CodeBase.Hero;

namespace CodeBase.Infrastracture.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            AllServices.Container.Register<IInputService>(new UnityInputService());
            
            
            //start gameloop
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
            
        }
    }
}