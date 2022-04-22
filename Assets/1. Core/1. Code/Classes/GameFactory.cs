using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class GameFactory : IGameFactory {
        private readonly IAssetProvider _assets;
        private readonly DiContainer _diContainer;

        [Inject]
        public GameFactory(DiContainer diContainer, IAssetProvider assets) {
            _diContainer = diContainer;
            _assets = assets;
        }

        public GameObject CreateCell(GameObject at) {
            var cellPrefab = _assets.Load<CellView>(path: AssetPath.HeroPath).Result;
            var cell = _diContainer.InstantiatePrefabForComponent<CellView>(cellPrefab, at.transform);
            return cell.gameObject;
        }

        public void CreateHud() {
            var hudPrefab = _assets.Load<GameObject>(path: AssetPath.HudPath).Result;
            var hud = _diContainer.InstantiatePrefab(hudPrefab);
            // return hud;
        }
    }
}