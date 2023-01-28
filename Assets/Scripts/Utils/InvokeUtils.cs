using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public delegate IEnumerator DebounceDelegate();
    public static class InvokeUtils
    {
        public static DebounceDelegate Debounce(Action callback, float delayInSecond) => Debounce(callback, () => delayInSecond);
        public static DebounceDelegate Debounce(Action callback, Func<float> delayInSecond)
        {
            bool waiting = false;
            return () =>
            {
                IEnumerator coroutine()
                {
                    if (waiting) yield break;
                    waiting = true;

                    callback();
                    yield return new WaitForSeconds(delayInSecond());
                    waiting = false;
                };
                return coroutine();
            };
        }
    }
}