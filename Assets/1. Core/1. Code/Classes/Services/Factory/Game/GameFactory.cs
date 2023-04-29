using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Logic.Game;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class GameFactory : IGameFactory {
        private readonly IAssetsProvider _assetsProvider;
        private readonly DiContainer _diContainer;

        [Inject]
        public GameFactory(DiContainer diContainer, IAssetsProvider assetsProvider) {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }
        
        public async UniTask<GameView> CreateGameView(Game game, Transform at) {
            var gameViewPrefab = await _assetsProvider.Load<GameView>(AssetPath.GameViewPath);
            var gameView = _diContainer.InstantiatePrefabForComponent<GameView>(gameViewPrefab, at);
            await gameView.Initialize(game, this);
            return gameView;
        }

        public async UniTask<CellView> CreateCell(Transform at) {
            var cellPrefab = await _assetsProvider.Load<CellView>(AssetPath.CellPath);
            var cell = _diContainer.InstantiatePrefabForComponent<CellView>(cellPrefab, at);
            return cell;
        }

        public async UniTask<GameObject> CreateHud() {
            var hudPrefab = await _assetsProvider.Load<GameObject>(AssetPath.HudPath);
            var hud = _diContainer.InstantiatePrefab(hudPrefab);
            return hud;
        }

        public void Cleanup() { }
    }
}