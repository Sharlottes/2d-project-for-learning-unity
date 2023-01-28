using Assets.Scripts.Structs.Singleton;
using Assets.Scripts.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;


namespace Assets.Scripts
{
    public class EffectSoundPool : SingletonMonoBehaviour<EffectSoundPool>
    {
        public GameObject dummy;
        private ObjectPool<EffectSoundDummy> pool;

        protected override void Awake()
        {
            _instance = this;
            pool = ObjectPoolUtils.CreateMonoBehaviourPool<EffectSoundDummy>(dummy);
            DontDestroyOnLoad(gameObject);
        }
        public void Release(EffectSoundDummy dummy) => pool.Release(dummy);
        public void PlayAudio(AudioClip clip, float x, float y) => pool.Get().Init(clip, x, y);
    }
}