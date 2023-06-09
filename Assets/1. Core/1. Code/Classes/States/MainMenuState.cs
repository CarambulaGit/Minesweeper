using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure {
    public class MainMenuState : IState {
        private readonly ApplicationStateMachine _appStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IMainMenuUIFactory _uiFactory;

        public MainMenuState(ApplicationStateMachine appStateMachine, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IMainMenuUIFactory uiFactory) {
            _uiFactory = uiFactory;
            _appStateMachine = appStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter() {
            _loadingCurtain.Show();
            _sceneLoader.Load(Consts.MainMenu, OnLoaded);
        }

        public void Exit() { }

        private async void OnLoaded() {
            var mainMenuRoot = await _uiFactory.CreateUIRoot(GameObject.FindWithTag(Consts.MainMenuTag).transform);
            mainMenuRoot.Initialize(OpenLevel);
            _loadingCurtain.Hide();
        }

        private void OpenLevel(GameMode gameMode) {
            _appStateMachine.Enter<LoadLevelState, GameMode>(gameMode);
        }
    }
}