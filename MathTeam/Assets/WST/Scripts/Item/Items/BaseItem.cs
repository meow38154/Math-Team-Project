using UnityEngine;
using WST.EventBus;
using WST.Events;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.Items
{
    public class BaseItem : AbstractInteraction
    {        
        [field: SerializeField] public AbstractItemSo AbstractItemSo { get; private set; }
        public override void PickUp()
        {
            Bus<ItemAddEvent>.Raise(new ItemAddEvent(this));
        }
    }
}