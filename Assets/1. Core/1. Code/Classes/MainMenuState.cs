using CodeBase.Infrastructure.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure {
    public class MainMenuState : IPayloadedState<string> {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain) {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName) {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded() { }

        private void OpenLevel() {
            _gameStateMachine.Enter<LoadLevelState, string>(Consts.Game);
        }
    }
}