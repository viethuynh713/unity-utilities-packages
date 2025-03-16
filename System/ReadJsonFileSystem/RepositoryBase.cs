using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using VPackages.Utilities.LoadAssetHelper;

namespace VPackages.System.Configuration
{
    public abstract class RepositoryBase<K,V> : IConfig<K, V> where K : ConfigItemBase<V> 
    {
        protected List<K> Configs;
        protected abstract string PATH_CONFIG_FILE { get; set; }

        protected RepositoryBase(ILoadAssetHelper loadAssetHelper)
        {
            // TODO: Mã hóa và giải mã file khi đọc
            var textAsset = loadAssetHelper.LoadAsset<TextAsset>(PATH_CONFIG_FILE);
            this.Configs = JsonConvert.DeserializeObject<List<K>>(textAsset.text);
            loadAssetHelper.UnloadAsset(textAsset);
        }

        protected RepositoryBase()
        {
            Configs = new List<K>();
        }

        public virtual K GetItemById(V itemId)
        {
            return Configs.FirstOrDefault(item => item.Id.Equals(itemId));
        }

        public virtual IReadOnlyList<K> GetAllItems()
        {
            return Configs;
        }

        public virtual IReadOnlyList<K> GetItemByCondition(Predicate<K> condition)
        {
            return Configs.Where(i => condition(i)).ToList();
        }
    }
}