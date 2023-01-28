using UnityEditor;
using UnityEngine;
public struct Vector2Test
{
    public decimal X, Y;

};

namespace Assets.Scripts.Structs.Singleton
{


    public class DDOLSingletonMonoBehaviour<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}