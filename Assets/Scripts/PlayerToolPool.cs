using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Tools;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class PlayerToolPool : SingletonMonoBehaviour<PlayerToolPool>
    {
        [SerializeField] private GameObject emptyTool;

        private ObjectPool<ITool> pool;

        protected override void Awake()
        {
            pool = ObjectPoolUtils.CreateMonoBehaviourPool<ITool>(emptyTool);
        }
        
        public T GetTool<T>() where T : ITool {
            T tool = pool.Get() as T;
            tool.Init();
            return tool;
        }

        // Release 어케하지
        public void RelaseTool(ITool tool) => pool.Release(tool);
    }
}