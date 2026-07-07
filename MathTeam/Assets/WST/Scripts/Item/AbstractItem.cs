using System;
using Player;
using UnityEngine;
using WST.EventBus;
using WST.Events;
using WST.Scripts.Item.ItemSOs;
using WST.Scripts.Item.ItemUI;

namespace WST.Scripts.Item
{
    public abstract class AbstractItem : AbstractPlayerRay
    {
        [field: SerializeField] public AbstractItemSo AbstractItemSo { get; private set; }
        protected override void RayInteraction(Transform target)
        {
            Bus<ItemAddEvent>.Raise(new ItemAddEvent(this));
        }
    }
}