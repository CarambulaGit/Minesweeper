using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.Windows.MainMenu;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory {
    public class MainMenuUIFactory : IMainMenuUIFactory {
        private IAssetsProvider _assetsProvider;
        private DiContainer _diContainer;
        private MainMenu _uiRoot;

        [Inject]
        public MainMenuUIFactory(DiContainer diContainer, IAssetsProvider assetsProvider) {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }

        public async UniTask<MainMenu> CreateUIRoot(Transform at) {
            var rootPrefab = await _assetsProvider.Load<GameObject>(AssetPath.MainMenuRootPath);
            _uiRoot = _diContainer.InstantiatePrefabForComponent<MainMenu>(rootPrefab, at);
            return _uiRoot;
        }

        public void Cleanup() {
            _uiRoot = null;
        }
    }
}