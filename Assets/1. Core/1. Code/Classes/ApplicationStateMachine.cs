using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using Zenject;

namespace CodeBase.Infrastructure {
    public class ApplicationStateMachine {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public ApplicationStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
            IMainMenuUIFactory mainMenuUIFactory, IGameEndUIFactory gameEndUIFactory) {
            _states = new Dictionary<Type, IExitableState> {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, loadingCurtain, mainMenuUIFactory),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, gameFactory),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(GameEndState)] = new GameEndState(this, gameEndUIFactory),
            };
        }

        public void Enter<TState>() where TState : class, IState {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState, TPayload1, TPayload2>(TPayload1 payload1, TPayload2 payload2) where TState : class, IPayloadedState<TPayload1, TPayload2> {
            TState state = ChangeState<TState>();
            state.Enter(payload1, payload2);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}