using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.Windows.MainMenu;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class MainMenuUIFactory : IMainMenuUIFactory
    {
        private IAssetsProvider _assetsProvider;
        private DiContainer _diContainer;
        private MainMenu _uiRoot;

        [Inject]
        public MainMenuUIFactory(DiContainer diContainer, IAssetsProvider assetsProvider) {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
        }
        
        public async UniTask<MainMenu> CreateUIRoot()
        {
            var rootPrefab = await _assetsProvider.Load<GameObject>(AssetPath.MainMenuRootPath);
            var uiParent = GameObject.FindWithTag(Consts.MainMenuTag).transform;
            _uiRoot = _diContainer.InstantiatePrefabForComponent<MainMenu>(rootPrefab, uiParent);
            return _uiRoot;
        }

        public void Cleanup()
        {
            _uiRoot = null;
        }
    }
}