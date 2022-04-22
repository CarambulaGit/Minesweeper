using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement {
    public interface IAssetProvider : IService {
        Task<T> Load<T>(string path) where T : Object;
    }
}