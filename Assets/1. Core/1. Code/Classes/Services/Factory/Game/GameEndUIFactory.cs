using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.Windows.MainMenu;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class GameEndUIFactory : IGameEndUIFactory {
        private IAssetsProvider _assetsProvider;
        private DiContainer _diContainer;
        private GameEnd _gameEnd;

        [Inject]
        public GameEndUIFactory(DiContainer diContainer, IAssetsProvider assetsProvider) {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }
        
        public async UniTask<GameEnd> CreateGameEndUI(Transform at) {
            var gameEndPrefab = await _assetsProvider.Load<GameObject>(AssetPath.GameEndUIPath);
            _gameEnd = _diContainer.InstantiatePrefabForComponent<GameEnd>(gameEndPrefab, at);
            return _gameEnd;
        }

        public void Cleanup() { }
    }
}