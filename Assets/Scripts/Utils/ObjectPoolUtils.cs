using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Utils
{
    public static class ObjectPoolUtils
    {
        public static ObjectPool<T> CreateMonoBehaviourPool<T>() where T : MonoBehaviour => CreateMonoBehaviourPool<T>(new GameObject());
        public static ObjectPool<T> CreateMonoBehaviourPool<T>(GameObject dummyPrefab) where T : MonoBehaviour
        {
            return new(
                () => Object.Instantiate(dummyPrefab).GetComponent<T>(),
                @object => @object.gameObject.SetActive(true),
                @object => @object.gameObject.SetActive(false),
                @object => Object.Destroy(@object.gameObject)
            );
        }
    }
}