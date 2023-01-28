
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Structs;
using Assets.Scripts.Keybind;
using Assets.Scripts.Structs.Singleton;

namespace Assets.Scripts
{
    delegate KeyBindManager KeysBindDelegate(Action<KeyCode[]> callback);

    class KeyBindManager : LazyDDOLSingletonMonoBehaviour<KeyBindManager>
    {
        private readonly Dictionary<Conditional, Action> binds = new();

        private void Update()
        {
            foreach (Conditional bind in binds.Keys)
            {
                if (!bind.condition()) continue;
                binds[bind]();
            }
        }

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
    }
}