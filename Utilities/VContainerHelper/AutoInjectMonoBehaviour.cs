using System;
using System.Collections;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace VPackages.Utilities.VContainerHelper
{
    public abstract class AutoInjectMonoBehaviour : MonoBehaviour
    {
        [Inject] protected IObjectResolver Container;    
        private const float TimeOut = 3f;

        protected virtual void Awake()
        {
            StartCoroutine(ResolveAll());
        }    
        IEnumerator ResolveAll()    
        {       
            if (Container != null) yield break;
            var lifetime = FindObjectOfType<LifetimeScope>();
            if (!lifetime)
            {
                lifetime = new GameObject("Lifetime Scope").AddComponent<LifetimeScope>();
            }
            float currentTime = 0;        
            yield return new WaitUntil(
                () =>
                {
                    currentTime += Time.deltaTime;
                    if (currentTime >= TimeOut) throw new Exception("Not Found LifetimeScope in Scene"); 
                    return lifetime != null;
                });        
            Container = lifetime.Container; 
            Container.Inject(this);  
            OnDependenciesResolved();    
        }    
        protected abstract void OnDependenciesResolved();
    }
}