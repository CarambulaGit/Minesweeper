using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure {
    public class GameRunner : MonoBehaviour, ICoroutineRunner {
        private Application _app;
        private LoadingCurtain _curtain;
        private IGameFactory _gameFactory;
        private IMainMenuUIFactory _mainMenuUIFactory;
        private IGameEndUIFactory _gameEndUIFactory;

        // [Inject]
        private void Construct(LoadingCurtain curtain, IGameFactory gameFactory, IMainMenuUIFactory mainMenuUIFactory,
            IGameEndUIFactory gameEndUIFactory) {
            _gameEndUIFactory = gameEndUIFactory;
            _mainMenuUIFactory = mainMenuUIFactory;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        private void Start() {
            _app = new Application(this, _curtain, _gameFactory, _mainMenuUIFactory, _gameEndUIFactory);
            _app.StateMachine.Enter<BootstrapState>();
        }
    }
}