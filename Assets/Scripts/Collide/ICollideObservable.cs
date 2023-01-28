using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICollideObservable
    {
        public GameObject GetGameObject();
        public void OnCollided(GameObject gameObject);
    }
}