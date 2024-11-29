using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Konzit.Core.Observer
{
    public class TopDownNotifier : INotifier
    {
        private List<IListener> _listener;

        public void AssignListener(IListener listener)
        {
            if(_listener == null)
                _listener = new List<IListener>();

            _listener.Add(listener);
        }

        public void NotiToAllListener()
        {
            if (_listener.Count == 0) return;

            foreach(IListener listener in _listener)
            {
                listener.OnReachNoti();
            }
        }

        public void NotiToTargetListener(IListener listener)
        {
            if (!_listener.Contains(listener)) return;
            
            // Noti
        }

        public void RemoveListener(IListener listener)
        {
            if (!_listener.Contains(listener)) return;

            _listener.Remove(listener);
        }
    }

}
