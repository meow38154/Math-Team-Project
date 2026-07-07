using WST.EventBus;
using WST.Scripts.Item;
using WST.Scripts.Item.Items;

namespace WST.Events
{
    public struct ItemAddEvent : IEvent
    {
        public AbstractItem Item;
        
        public ItemAddEvent(AbstractItem item)
        {
            Item = item;
        }
    }
}