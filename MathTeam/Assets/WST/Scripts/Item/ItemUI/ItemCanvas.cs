using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                itemContainer.LeftMove();
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                itemContainer.RightMove();
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (itemContainer.CanUseItem())
                    itemContainer.UseItem();
            }
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