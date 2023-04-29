using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using Zenject;

namespace CodeBase.Infrastructure {
    public class Application {
        public readonly ApplicationStateMachine StateMachine;

        [Inject]
        public Application(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, IGameFactory gameFactory,
            IMainMenuUIFactory mainMenuUIFactory, IGameEndUIFactory gameEndUIFactory) {
            var sceneLoader = new SceneLoader(coroutineRunner);
            StateMachine = new ApplicationStateMachine(sceneLoader, curtain, gameFactory, mainMenuUIFactory, gameEndUIFactory);
        }
    }
}