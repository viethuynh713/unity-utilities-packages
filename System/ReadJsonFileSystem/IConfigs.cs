using System;
using System.Collections.Generic;

namespace VPackages.System.Configuration
{
    public interface IConfig<K,V> where K : ConfigItemBase<V> 
    {
        K GetItemById(V itemId);
        IReadOnlyList<K> GetAllItems();
        IReadOnlyList<K> GetItemByCondition(Predicate<K> condition);
    }
}