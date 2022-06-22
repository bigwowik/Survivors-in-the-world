namespace CodeBase.Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;

    }
}