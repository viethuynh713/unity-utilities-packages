using System;

namespace VPackages.System.Configuration
{
    public class ConfigItemBase<T> 
    {
        public T Id { get; }

        protected ConfigItemBase(T id)
        {
            if (!(id is int || id is float || id is string || id is double || id is bool))
            {
                throw new ArgumentException("ID must be int, float, string, double, or bool.");
            }
            Id = id;
        }
    }
}