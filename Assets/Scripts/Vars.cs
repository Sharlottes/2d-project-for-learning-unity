using Assets.Scripts.Structs.Singleton;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Vars : DDOLSingletonMonoBehaviour<Vars>
    {
        public Player CurrentPlayer;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}