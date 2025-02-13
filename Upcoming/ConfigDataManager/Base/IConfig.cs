using System;
using System.Collections.Generic;

namespace Kozits.ZooCoffee.Interfaces
{
    public interface IConfig<T> 
    {
        T GetItemById(string itemId);
        IReadOnlyList<T> GetAllItems();
        IReadOnlyList<T> GetItemByCondition(Predicate<T> condition);
    }
}