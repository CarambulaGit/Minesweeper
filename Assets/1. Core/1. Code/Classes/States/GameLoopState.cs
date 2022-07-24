using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure {
    public class GameLoopState : IState {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine) {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter() { }

        public void Exit() { }

        private void EnterMainMenu() =>
            _gameStateMachine.Enter<MainMenuState>();
    }
}