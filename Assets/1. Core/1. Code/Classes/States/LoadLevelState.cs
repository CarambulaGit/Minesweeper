using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Logic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States {
    public class LoadLevelState : IPayloadedState<GameMode> {
        private readonly ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private GameMode _gameMode;

        public LoadLevelState(ApplicationStateMachine appStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory) {
            _stateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(GameMode game) {
            _gameMode = game;
            _loadingCurtain.Show();
            _sceneLoader.Load(Consts.Game, () => OnLoaded());
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private async UniTask OnLoaded() {
            // TODO
            var game = new Game(_gameMode);
            await _gameFactory.CreateGameView(game, GameObject.FindWithTag(Consts.GameViewTag).transform);
            // _gameFactory.CreateHud();
            _stateMachine.Enter<GameLoopState, Game>(game);
        }
    }
}