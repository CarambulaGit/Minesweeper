using CodeBase.Infrastructure.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure {
    public class MainMenuState : IState {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain) {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter() {
            _loadingCurtain.Show();
            _sceneLoader.Load(Consts.MainMenu, OnLoaded);
        }

        public void Exit() {
            
        }

        private void OnLoaded() => _loadingCurtain.Hide();

        private void OpenLevel() {
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}