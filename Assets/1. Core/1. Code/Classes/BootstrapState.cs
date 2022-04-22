using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure {
    public class BootstrapState : IState {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() {
            _sceneLoader.Load(Consts.Boot, onLoaded: EnterMainMenu);
        }

        public void Exit() { }

        private void EnterMainMenu() =>
            _stateMachine.Enter<MainMenuState, string>(Consts.MainMenu);
    }
}