using Player;
using UnityEngine;
using WST.EventBus;
using WST.Events;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.Items
{
    public abstract class AbstractInteraction : MonoBehaviour, IPickUpable
    {
        public abstract void PickUp();
    }
}