using System;
using System.Collections.Generic;
using System.ComponentModel;
using Zenject;

namespace CodeBase.Infrastracture.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private IState _activeState;
        private Dictionary<Type, IState> allStates;

        public GameStateMachine(SceneLoader sceneLoader, IFactory factory)
        {
            allStates = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader, factory),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private IState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return (TState) allStates[typeof(TState)];
        }
    }
}