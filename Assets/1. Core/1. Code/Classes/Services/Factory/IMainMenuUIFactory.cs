using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows.MainMenu;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Factory
{
    public interface IMainMenuUIFactory: IService
    {
        UniTask<MainMenu> CreateUIRoot();
        void Cleanup();
    }
}