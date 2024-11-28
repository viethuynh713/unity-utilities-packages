using UnityEngine;
using VPackages.Core.ObserverPattern.impl;

namespace VPackages.Core.ObserverPattern.Sample
{
    public class SubjectMonoBehavior : MonoBehaviour
    {
        private IEventManager _eventManager;

        private void Start()
        {
            _eventManager = EventManagerSingleton.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _eventManager.PostEvent(EventID.HelloWorld, "Hello World");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _eventManager.PostEvent(EventID.NoParam, "Hello World");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _eventManager.PostEvent(EventID.NoParam);
            }
        }
    }
}