using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemContainer : MonoBehaviour
    {
        private List<ItemSlotUI> _itemSlots;


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _itemSlots = GetComponentsInChildren<ItemSlotUI>().ToList();
            foreach (ItemSlotUI slot in _itemSlots)
            {
                slot.Init();
            }
        }
    }
}