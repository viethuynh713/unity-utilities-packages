using System;
using System.Collections.Generic;
using UnityEngine;

namespace VPackages.Core.ObserverPattern.impl
{
    public class EventManagerBase : IEventManager
    {
        #region Fields

        private Dictionary<EventID, Delegate> _listeners = new();

        #endregion


        #region Add Listeners, Post events, Remove listener

        public void RegisterListener<T>(EventID eventID, Action<T> callback)
        {
            if (!_listeners.ContainsKey(eventID))
            {
                _listeners.Add(eventID, null);
            }

            // check if listener exist in distionary
            if (_listeners[eventID] is Action<T> existingDelegate)
            {
                // add callback to our collection
                _listeners[eventID] = existingDelegate + callback;
            }
            else if (_listeners[eventID] == null)
            {
                // add new key-value pair
                _listeners[eventID] = callback;
            }
            else
            {
                Debug.LogError($"Event '{eventID}' parameter type mismatch. Expected type {typeof(T)}.");
                return;
            }
        }

        public void RegisterListener(EventID eventID, Action callback)
        {
            if (!_listeners.ContainsKey(eventID))
            {
                _listeners.Add(eventID, null);
            }

            // check if listener exist in distionary
            if (_listeners[eventID] is Action existingDelegate)
            {
                // add callback to our collection
                _listeners[eventID] = existingDelegate + callback;
            }
            else if (_listeners[eventID] == null)
            {
                // add new key-value pair
                _listeners[eventID] = callback;
            }
            else
            {
                Debug.LogError($"Expect no parameter in Event '{eventID}'.");
                return;
            }
        }

        public void PostEvent<T>(EventID eventID, T param)
        {
            if (!_listeners.ContainsKey(eventID))
            {
                Debug.Log($"No listeners for this event : {eventID}");
                return;
            }

            // posting event
            if (_listeners[eventID] is Action<T> action)
            {
                action.Invoke(param);
            }
            else
            {
                Debug.LogError($"No listeners found for event '{eventID}' or type mismatch.");
            }
        }

        public void PostEvent(EventID eventID)
        {
            if (!_listeners.ContainsKey(eventID))
            {
                Debug.Log($"No listeners for this event : {eventID}");
                return;
            }

            // posting event
            if (_listeners[eventID] is Action action)
            {
                action.Invoke();
            }
            else
            {
                Debug.LogError($"No listeners found for event '{eventID}' or type mismatch.");
            }
        }

        public void RemoveListener<T>(EventID eventID, Action<T> callback)
        {
            if (_listeners.ContainsKey(eventID))
            {
                if (_listeners[eventID] is Action<T> existingDelegate)
                {
                    existingDelegate -= callback;
                    if (existingDelegate == null)
                    {
                        _listeners.Remove(eventID);
                    }
                    else
                    {
                        _listeners[eventID] = existingDelegate;
                    }
                }
                else
                {
                    Debug.Log($"Miss match type of event {eventID}");
                }
            }
            else
            {
                Debug.LogWarning($"RemoveListener, not found key : " + eventID);
            }
        }

        public void RemoveListener(EventID eventID, Action callback)
        {
            if (_listeners.ContainsKey(eventID))
            {
                if (_listeners[eventID] is Action existingDelegate)
                {
                    existingDelegate -= callback;
                    if (existingDelegate == null)
                    {
                        _listeners.Remove(eventID);
                    }
                    else
                    {
                        _listeners[eventID] = existingDelegate;
                    }
                }
                else
                {
                    Debug.Log($"Miss match type of event {eventID}");
                }
            }
            else
            {
                Debug.LogWarning($"RemoveListener, not found key : " + eventID);
            }
        }

        public void ClearAllListener()
        {
            _listeners.Clear();
        }

        #endregion
    }
}