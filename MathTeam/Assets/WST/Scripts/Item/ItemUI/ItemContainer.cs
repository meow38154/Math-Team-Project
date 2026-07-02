using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlots;

        private int _nowIdx = 0;
        
        public ItemSlotUI NowItemSlot => itemSlots[_nowIdx];

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach (ItemSlotUI slot in itemSlots)
            {
                slot.Init();
            }
        }

        public void AddItem()
        {
            
        }

        public void Left() => _nowIdx = Math.Max(0, _nowIdx);
        public void Right() => _nowIdx = Math.Min(itemSlots.Count - 1, _nowIdx);
    }
}