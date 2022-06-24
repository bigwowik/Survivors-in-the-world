using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public readonly IGameStateMachine GameStateMachine;


        public Game(IGameStateMachine gameStateMachine)
        {
            this.GameStateMachine = gameStateMachine;
        }
    }
}