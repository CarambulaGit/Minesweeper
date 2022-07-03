using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class GameFactory : IGameFactory {
        private readonly IAssetsProvider _assetses;
        private readonly DiContainer _diContainer;

        [Inject]
        public GameFactory(DiContainer diContainer, IAssetsProvider assetses) {
            _diContainer = diContainer;
            _assetses = assetses;
        }

        public GameObject CreateCell(GameObject at) {
            var cellPrefab = _assetses.Load<CellView>(path: AssetPath.HeroPath).Result;
            var cell = _diContainer.InstantiatePrefabForComponent<CellView>(cellPrefab, at.transform);
            return cell.gameObject;
        }

        public void CreateHud() {
            var hudPrefab = _assetses.Load<GameObject>(path: AssetPath.HudPath).Result;
            var hud = _diContainer.InstantiatePrefab(hudPrefab);
            // return hud;
        }
    }
}