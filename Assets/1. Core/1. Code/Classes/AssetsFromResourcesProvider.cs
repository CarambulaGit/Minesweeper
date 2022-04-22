using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement {
    public class AssetsFromResourcesProvider : IAssetProvider {
        public async Task<T> Load<T>(string path) where T : Object {
            var loadTask = Resources.LoadAsync<T>(path);
            while (!loadTask.isDone) {
                await Task.Yield();
            }

            return loadTask.asset as T;
        }
    }
}