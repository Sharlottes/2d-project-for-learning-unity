using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Keybind
{
    public interface IKeyBind
    {
        public KeyCode[] GetKeys();
    }
}