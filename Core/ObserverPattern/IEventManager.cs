using System;

namespace VPackages.Core.ObserverPattern
{
    public interface IEventManager
    {
        void RegisterListener<T>(EventID eventID, Action<T> callback);
        void RegisterListener(EventID eventID, Action callback);
        void PostEvent<T>(EventID eventID, T param);
        void PostEvent(EventID eventID);
        void RemoveListener<T>(EventID eventID, Action<T> callback);
        void RemoveListener(EventID eventID, Action callback);
        
        void ClearAllListener();
    }
}