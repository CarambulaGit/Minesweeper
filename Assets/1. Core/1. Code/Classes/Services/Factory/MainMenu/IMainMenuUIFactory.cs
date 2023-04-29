using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows.MainMenu;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory {
    public interface IMainMenuUIFactory : IService {
        UniTask<MainMenu> CreateUIRoot(Transform at);
        void Cleanup();
    }
}