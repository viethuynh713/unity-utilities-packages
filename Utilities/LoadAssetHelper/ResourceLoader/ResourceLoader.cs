using Cysharp.Threading.Tasks;
using UnityEngine;
namespace VPackages.Utilities.LoadAssetHelper
{
    public class ResourceLoader: ILoadAssetHelper
    {
        public T LoadAsset<T>(string assetPath) where T : Object
        {
            return Resources.Load<T>(assetPath);
        }

        public void UnloadAsset(Object asset)
        {
            Resources.UnloadAsset(asset);
        }
    }
}