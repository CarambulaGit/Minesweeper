using CodeBase.Infrastructure.Logic.Game;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory {
    public interface IGameFactory : IService {
        UniTask<GameView> CreateGameView(Game game, Transform at);
        UniTask<CellView> CreateCell(Transform at);
        UniTask<GameObject> CreateHud();
        void Cleanup();
    }
}