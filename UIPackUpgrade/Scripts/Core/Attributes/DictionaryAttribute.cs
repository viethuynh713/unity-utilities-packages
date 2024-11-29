using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Konzit.Core.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DictionaryAttribute : PropertyAttribute
    {
        public Dictionary<object, object> dict = new Dictionary<object, object> ();
        //public DictionaryAttribute<T>(Dictionary<T, T> dictionary)
        //{

        //}
        public DictionaryAttribute()
        {
            //this.dict = dict;
        }
    }

    [CustomPropertyDrawer(typeof(Dictionary<Type, Type>))]
    public class DictionaryElementViewInEditor : PropertyDrawer
    {

    }

    public class DictionaryElementView <T>
    {
        public T Key;
        public T Element;
    }
}
