using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.Infrastructure {
    public class MainMenuState : IState {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IMainMenuUIFactory _uiFactory;

        public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IMainMenuUIFactory uiFactory) {
            _uiFactory = uiFactory;
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

        private async void OnLoaded()
        {
            var mainMenuRoot = await _uiFactory.CreateUIRoot();
            mainMenuRoot.Initialize(OpenLevel);
            _loadingCurtain.Hide();
        }

        private void OpenLevel(GameMode gameMode) {
            _gameStateMachine.Enter<LoadLevelState, GameMode>(gameMode);
        }
    }
}