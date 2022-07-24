using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure {
    public class GameRunner : MonoBehaviour, ICoroutineRunner {
        private Game _game;
        private LoadingCurtain _curtain;
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(LoadingCurtain curtain, IGameFactory gameFactory) {
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        private void Start() {
            _game = new Game(this, _curtain, _gameFactory);
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}