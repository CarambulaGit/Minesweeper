using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure {
    public class GameEndState : IPayloadedState<GameMode, bool> {
        private readonly ApplicationStateMachine _appStateMachine;
        private readonly IGameEndUIFactory _gameEndUIFactory;
        private GameMode _gameMode;
        private GameEnd _gameEnd;
        private bool _isWin;

        public GameEndState(ApplicationStateMachine appStateMachine, IGameEndUIFactory gameEndUIFactory) {
            _appStateMachine = appStateMachine;
            _gameEndUIFactory = gameEndUIFactory;
        }

        public void Enter(GameMode gameMode, bool isWin) {
            _isWin = isWin;
            _gameMode = gameMode;
            CreateGameEndUI();
        }

        public void Exit() { }

        private async UniTask CreateGameEndUI() {
            _gameEnd = await _gameEndUIFactory.CreateGameEndUI(GameObject.FindWithTag(Consts.GameEndTag).transform);
            _gameEnd.Initialize(Restart, EnterMainMenu, _isWin);
        }

        private void EnterMainMenu() =>
            _appStateMachine.Enter<MainMenuState>();

        private void Restart() =>
            _appStateMachine.Enter<LoadLevelState, GameMode>(_gameMode);
    }
}