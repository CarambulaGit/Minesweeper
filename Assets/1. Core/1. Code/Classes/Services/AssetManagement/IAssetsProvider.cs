using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement {
    public interface IAssetsProvider : IService {
        Task<T> Load<T>(string path) where T : Object;
    }

    // class AssetsFromAddressableProvider : IAssetProvider {
    //     public Task<T> Load<T>(AssetReference path) where T : Object {
    //         throw new System.NotImplementedException();
    //     }
    // }
}