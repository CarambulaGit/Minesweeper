using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory {
    public interface IGameFactory : IService {
        UniTask<GameObject> CreateCell(GameObject at);
        UniTask<GameObject> CreateHud();
    }
}