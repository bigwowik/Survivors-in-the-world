using CodeBase.Infrastracture.States;

namespace CodeBase.Infrastracture
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