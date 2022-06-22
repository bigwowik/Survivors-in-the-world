using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public readonly IGameStateMachine gameStateMachine;


        public Game(IGameStateMachine gameStateMachine)
        {
            
            this.gameStateMachine = gameStateMachine;
        }
    }
}