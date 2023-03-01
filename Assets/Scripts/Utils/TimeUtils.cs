using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public static class TimeUtils
    {
        public static IEnumerator WaitForSecondsCoroutine(Action callback, float duration) 
        {
            yield return new WaitForSeconds(duration);
            callback();
        }
        public static void SetTimeout(Action callback, float duration) => GlobalMonobehaviour.Instance.StartCoroutine(WaitForSecondsCoroutine(callback, duration));
    }
}
