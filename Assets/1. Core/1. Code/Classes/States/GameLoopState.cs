using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure {
    public class GameLoopState : IPayloadedState<Game> {
        private readonly ApplicationStateMachine _appStateMachine;
        private Game _game;

        public GameLoopState(ApplicationStateMachine appStateMachine) {
            _appStateMachine = appStateMachine;
        }

        public void Enter(Game game) {
            _game = game;
            _game.GameEndEvent += EnterGameEndState;
        }

        public void Exit() => _game.GameEndEvent -= EnterGameEndState;

        private void EnterGameEndState(bool isWin) =>
            _appStateMachine.Enter<GameEndState, GameMode, bool>(_game.GameMode, isWin);
    }
}