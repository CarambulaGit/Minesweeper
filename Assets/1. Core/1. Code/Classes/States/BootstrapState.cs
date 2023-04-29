using CodeBase.Infrastructure.States;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure {
    public class BootstrapState : IState {
        private ApplicationStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(ApplicationStateMachine stateMachine, SceneLoader sceneLoader) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() {
            var bootSceneName = Consts.Boot;
            if (SceneManager.GetActiveScene().name == bootSceneName) {
                EnterMainMenu();
                return;
            }
            
            _sceneLoader.Load(bootSceneName, onLoaded: EnterMainMenu);
        }

        public void Exit() { }

        private void EnterMainMenu() =>
            _stateMachine.Enter<MainMenuState>();
    }
}