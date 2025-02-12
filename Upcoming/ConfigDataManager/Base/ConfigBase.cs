using System;
using System.Collections.Generic;
using System.Linq;
using Kozits.ZooCoffee.Interfaces;
using UnityEngine;
// Chỉnh sửa sau
namespace Kozits.ZooCoffee.Config
{
    public abstract class ConfigBase<T>: ScriptableObject, IConfig<T> where T : ItemBase
    {
        [SerializeField] protected List<T> items = new List<T>();
        
        public virtual void AddItem(T item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        public virtual bool RemoveItem(T item)
        {
            return items.Remove(item);
        }

        public virtual T GetItemById(string itemId)
        {
            return items.FirstOrDefault(i => i.id == itemId);
        }
        public virtual IReadOnlyList<T> GetAllItems()
        {
            return items.AsReadOnly();
        }

        public virtual IReadOnlyList<T> GetItemByCondition(Predicate<T> condition)
        {
            return items.FindAll(condition).AsReadOnly();
        }
    }
}