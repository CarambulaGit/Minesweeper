using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.States {
    public class LoadLevelState : IState {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory) {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter() {
            _loadingCurtain.Show();
            _sceneLoader.Load(Consts.Game, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded() {
            // TODO
            // GameObject hero = _gameFactory.CreateCell(GameObject.FindWithTag(InitialPointTag));
            // _gameFactory.CreateHud();
            // _stateMachine.Enter<GameLoopState>();
        }
    }
}