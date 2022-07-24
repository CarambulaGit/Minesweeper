using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure.States {
    public class LoadLevelState : IPayloadedState<GameMode> {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private GameMode _gameMode;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory) {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(GameMode gameMode) {
            _gameMode = gameMode;
            _loadingCurtain.Show();
            _sceneLoader.Load(Consts.Game, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded() {
            // TODO
            // GameObject hero = _gameFactory.CreateCell(GameObject.FindWithTag(InitialPointTag));
            // _gameFactory.CreateHud();
            _stateMachine.Enter<GameLoopState>();
            GameObject.Find("GameMode").GetComponent<TMP_Text>().text = $"{_gameMode.GameDifficulty}\n{_gameMode}";
        }
    }
}