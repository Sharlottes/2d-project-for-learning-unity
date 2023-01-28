using Assets.Scripts;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using Assets.Scripts.Structs.Singleton;

namespace Assets.Scripts
{
    public class CollideObserver : LazyDDOLSingletonMonoBehaviour<CollideObserver>
    {
        private Dictionary<ICollideObservable, Func<CollideObservableOptions>> observables = new();

        private void Update()
        {
            foreach ((ICollideObservable observable, Func<CollideObservableOptions> getter) in observables)
            {
                CollideObservableOptions options = getter();
                RaycastHit2D[] hits = Physics2D.RaycastAll(options.origin, options.direction, options.distance);
                
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.rigidbody == null || hit.rigidbody.gameObject == observable.GetGameObject()) continue;
                    observable.OnCollided(hit.rigidbody.gameObject);
                    break;
                }
            }
        }

        public void SubscribeCollider(ICollideObservable observable, bool updatable = false) {
            if (updatable) SubscribeCollider(observable, () => new()
            {
                origin = observable.GetGameObject().transform.position,
                direction = observable.GetGameObject().transform.forward,
            });
            else SubscribeCollider(observable, new CollideObservableOptions()
            {
                origin = observable.GetGameObject().transform.position,
                direction = observable.GetGameObject().transform.forward,
            });
        }
        public void SubscribeCollider(ICollideObservable observable, CollideObservableOptions getter) => SubscribeCollider(observable, () => getter);
        public void SubscribeCollider(ICollideObservable observable, Func<CollideObservableOptions> getter)
        {
            observables.Add(observable, getter);
        }
        public void UnSubscribeCollider(ICollideObservable observable)
        {
            observables.Remove(observable);
        }
    }
}