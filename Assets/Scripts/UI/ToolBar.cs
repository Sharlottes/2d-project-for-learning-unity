using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ToolBar : MonoBehaviour
    {
        private int _maxTools = 5;
        public int MaxTools
        {
            get => _maxTools;
            set
            {
                _maxTools = value;
                ReconclizeSlots();
            }
        }
        public GameObject toolSlots;
        private Stack<ToolSlot> slots = new();

        private void Awake()
        {
            InternalUtils.Repeat(_maxTools, i =>
            {
                ToolSlot slot = Instantiate(toolSlots, transform).GetComponent<ToolSlot>();
                slot.Init(i);
                slots.Push(slot);
            });
        }

        private void ReconclizeSlots()
        {
            int currentLength = slots.Count;
            InternalUtils.Repeat(currentLength - _maxTools, _ => slots.Pop());
            InternalUtils.Repeat(_maxTools - currentLength, i =>
            {
                ToolSlot slot = Instantiate(toolSlots, transform).GetComponent<ToolSlot>();
                slot.Init(i);
                slots.Push(slot);
            });
        }

        public 
    }
}
