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

        private Dictionary<int, AbstractItemSo> itemDict = new();

        public void Init()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i].Init();
                itemDict.Add(i, null);
            }

            SelectItem();
        }

        public bool AddItem(AbstractItemSo itemSo)
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (itemDict[i] == null)
                {
                    itemDict[i] = itemSo;
                    itemSlots[i].AddItem(itemSo.ItemSprite);
                    return true;
                }
            }

            return false;
        }

        public void UseItem()
        {
            itemDict[_nowIdx].RaiseEvent();
            itemDict[_nowIdx] = null;
            itemSlots[_nowIdx].AddItem(null);
        }

        public bool CanUseItem()
        {
            return itemDict[_nowIdx] != null;
        }

        private void SelectItem()
        {
            foreach (ItemSlotUI slot in itemSlots)
            {
                slot.Select(false);
            }
            itemSlots[_nowIdx].Select(true);
        }

        public void LeftMove()
        {
            _nowIdx = Math.Max(0, _nowIdx - 1);
            SelectItem();
        }

        public void RightMove()
        {
            _nowIdx = Math.Min(itemSlots.Count - 1, _nowIdx + 1);
            SelectItem();
        }
    }
}