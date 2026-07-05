using System;
using UnityEngine;
using WST.EventBus;
using WST.Events;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemCanvas : MonoBehaviour
    {
        [SerializeField] private ItemContainer itemContainer;

        private void Awake()
        {
            itemContainer.Init();
            Bus<ItemAddEvent>.OnEvent += HandleItemAdd;
        }

        private void OnDestroy()
        {
            Bus<ItemAddEvent>.OnEvent -= HandleItemAdd;
        }

        private void HandleItemAdd(ItemAddEvent obj)
        {
            if (itemContainer.AddItem(obj.Item.AbstractItemSo))
            {
                Destroy(obj.Item.gameObject);
            }
        }
    }
}