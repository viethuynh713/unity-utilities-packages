using System;
using UnityEngine;

namespace VPackages.Core.ObserverPattern.impl
{
    public class EventManagerSingleton : MonoBehaviour, IEventManager
    {
        #region Singleton

        private static EventManagerSingleton _instance;
        private readonly IEventManager _bridge = new EventManagerBase();

        public static IEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<EventManagerSingleton>();
                    singletonObject.name = "Singleton - EventManager";
                }

                return _instance;
            }
        }

        public static bool HasInstance()
        {
            return _instance != null;
        }

        void Awake()
        {
            if (_instance != null && _instance.GetInstanceID() != this.GetInstanceID())
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }


        void OnDestroy()
        {
            if (_instance == this)
            {
                ClearAllListener();
                _instance = null;
            }
        }

        #endregion

        public void RegisterListener<T>(EventID eventID, Action<T> callback)
        {
            _bridge.RegisterListener(eventID, callback);
        }

        public void RegisterListener(EventID eventID, Action callback)
        {
            _bridge.RegisterListener(eventID, callback);
        }

        public void PostEvent<T>(EventID eventID, T param)
        {
            _bridge.PostEvent(eventID, param);
        }

        public void PostEvent(EventID eventID)
        {
            _bridge.PostEvent(eventID);
        }

        public void RemoveListener<T>(EventID eventID, Action<T> callback)
        {
            _bridge.RemoveListener(eventID, callback);
        }

        public void RemoveListener(EventID eventID, Action callback)
        {
            _bridge.RemoveListener(eventID, callback);
        }

        public void ClearAllListener()
        {
            _bridge.ClearAllListener();
        }
    }
}