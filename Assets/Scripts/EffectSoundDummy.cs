using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EffectSoundDummy : MonoBehaviour
    {
        AudioSource source;
        public void Awake()
        {
            source = GetComponent<AudioSource>();
        }
        public void Update()
        {
            if (!source.isPlaying) EffectSoundPool.Instance.Release(this);
        }
        public EffectSoundDummy Init(AudioClip clip, float x, float y)
        {
            transform.position = new(x, y);
            source.clip = clip;
            source.Play();
            return this;
        }
    }
}