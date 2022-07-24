using CodeBase.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly DiContainer _diContainer;

        [Inject]
        public GameFactory(DiContainer diContainer, IAssetsProvider assetsProvider)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }

        public async UniTask<GameObject> CreateCell(GameObject at)
        {
            var cellPrefab = await _assetsProvider.Load<CellView>(path: AssetPath.HeroPath);
            var cell = _diContainer.InstantiatePrefabForComponent<CellView>(cellPrefab, at.transform);
            return cell.gameObject;
        }

        public async UniTask<GameObject> CreateHud()
        {
            var hudPrefab = await _assetsProvider.Load<GameObject>(path: AssetPath.HudPath);
            var hud = _diContainer.InstantiatePrefab(hudPrefab);
            return hud;
        }

        public void Cleanup() { }
    }
}