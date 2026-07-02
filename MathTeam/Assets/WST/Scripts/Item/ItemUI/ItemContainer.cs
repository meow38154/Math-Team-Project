using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemSlotUI> itemSlots;


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
    }
}