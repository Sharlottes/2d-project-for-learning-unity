using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Tools;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class ToolPool : SingletonMonoBehaviour<ToolPool>
    {
        [SerializeField] private GameObject emptyTool;

        private ObjectPool<Tool> pool;

        protected override void Awake()
        {
            pool = ObjectPoolUtils.CreateMonoBehaviourPool<Tool>(emptyTool);
        }
        
        public T GetTool<T>() where T : Tool 
        {
            T tool = pool.Get() as T;
            tool.Init();
            return tool;
        }

        // Release 어케하지
        public void RelaseTool(Tool tool) => pool.Release(tool);
    }
}