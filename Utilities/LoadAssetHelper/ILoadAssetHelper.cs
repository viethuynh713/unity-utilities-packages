using UnityEngine;

namespace VPackages.Utilities.LoadAssetHelper
{
    public interface ILoadAssetHelper
    {
        T LoadAsset<T>(string assetPath) where T : Object;
        void UnloadAsset(Object asset);


    }
}