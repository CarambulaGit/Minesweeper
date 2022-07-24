using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;

namespace CodeBase.Infrastructure {
    public class Game {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory, IMainMenuUIFactory mainMenuUIFactory) {
            var sceneLoader = new SceneLoader(coroutineRunner);
            StateMachine = new GameStateMachine(sceneLoader, curtain, gameFactory, mainMenuUIFactory);
        }
    }
}