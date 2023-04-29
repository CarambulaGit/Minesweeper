using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory {
    public interface IGameEndUIFactory : IService {
        UniTask<GameEnd> CreateGameEndUI(Transform at);
        void Cleanup();
    }
}