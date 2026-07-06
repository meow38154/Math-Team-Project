using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlots;

        private int _nowIdx = 0;
        
        public ItemSlotUI NowItemSlot => itemSlots[_nowIdx];

        public void Init()
        {
            foreach (ItemSlotUI slot in itemSlots)
            {
                slot.Init();
            }
        }

        public bool AddItem(AbstractItemSo itemSo)
        {
            foreach (ItemSlotUI slot in itemSlots)
            {
                if (slot.ItemSo == null)
                {
                    slot.AddItem(itemSo);
                    return true;
                }
            }
            return false;
        }

        public void UseItem()
        {
            if (NowItemSlot.ItemSo != null)
            {
                NowItemSlot.ItemSo.RaiseEvent(); 
                NowItemSlot.AddItem(null);
            }
        }

        public void LeftMove() => _nowIdx = Math.Max(0, _nowIdx);
        public void RightMove() => _nowIdx = Math.Min(itemSlots.Count - 1, _nowIdx);
    }
}