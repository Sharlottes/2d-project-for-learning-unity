
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Structs;
using Assets.Scripts.Keybind;

namespace Assets.Scripts
{
    delegate KeyBindManager KeysBindDelegate(Action<KeyCode[]> callback);

    class KeyBindManager : MonoBehaviour
    {
        private static KeyBindManager _main;
        public static KeyBindManager Main => _main;

        private readonly Dictionary<Conditional, Action> binds = new();
        public KeysBindDelegate Bind() => Bind(new KeyBind((out KeyCode[] res) => {
            List<KeyCode> pressedKeys = new();
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(key)) pressedKeys.Add(key);
            }
            res = pressedKeys.ToArray();
            return true;
        }));
        public KeysBindDelegate Bind(params KeyCode[] keys) => Bind(new OrBind(keys));
        public KeysBindDelegate Bind(KeyCode key) => Bind(new KeyBind(key));
        public KeysBindDelegate Bind(params KeyBind[] binds) => Bind(new OrBind(binds));
        public KeysBindDelegate Bind(KeyBind bind) => (callback) =>
        {
            binds.Add(bind, () => {
                if (!bind.condition(out KeyCode[] res)) return;
                callback(res);
            });

            return this;
        };
        private void Awake()
        {
            // 이것이 바로 DDOL non-lazy singleton이다 희망편
            _main = this;
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        private void Update()
        {
            foreach (Conditional bind in binds.Keys)
            {
                if (!bind.condition()) continue;
                binds[bind]();
            }
        }
    }
}