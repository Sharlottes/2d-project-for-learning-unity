using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    class Tool : MonoBehaviour { }

    public class PlayerToolPool : SingletonMonoBehaviour<PlayerToolPool>
    {
        [SerializeField] private GameObject emptyTool;

        private ObjectPool<Tool> pool;

        protected override void Awake()
        {
            pool = ObjectPoolUtils.CreateMonoBehaviourPool<Tool>(emptyTool);
        }
    }
}