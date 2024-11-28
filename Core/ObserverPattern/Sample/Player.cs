using System;
using UnityEngine;
using VPackages.Core.ObserverPattern.impl;

namespace VPackages.Core.ObserverPattern.Sample
{
    public class Player : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManagerSingleton.Instance.RegisterListener<string>(EventID.HelloWorld, SayHello);
            EventManagerSingleton.Instance.RegisterListener(EventID.NoParam, CallEventNoParam);
        }

        private void CallEventNoParam()
        {
            Debug.Log("No param Event");
        }

        private void SayHello(string param)
        {
            Debug.Log($"Player give event and say: {param}");
        }

        private void OnDisable()
        {
            EventManagerSingleton.Instance.RemoveListener<string>(EventID.HelloWorld, SayHello);
            EventManagerSingleton.Instance.RemoveListener(EventID.NoParam, CallEventNoParam);
        }
    }
}