using Player;
using UnityEngine;
using WST.EventBus;
using WST.Events;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.Items
{
    public abstract class AbstractItem : MonoBehaviour, IPickUpable
    {
        [field: SerializeField] public AbstractItemSo AbstractItemSo { get; private set; }
        public void PickUp()
        {
            Debug.Log("ee");
            Bus<ItemAddEvent>.Raise(new ItemAddEvent(this));
        }
    }
}