using CodeBase.Infrastracture.States;

namespace CodeBase.Infrastracture
{
    public class Game
    {
        public readonly GameStateMachine gameStateMachine;


        public Game(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
    }
}