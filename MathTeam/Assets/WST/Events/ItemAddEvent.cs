using WST.EventBus;
using WST.Scripts.Item;
using WST.Scripts.Item.Items;

namespace WST.Events
{
    public struct ItemAddEvent : IEvent
    {
        public BaseItem Interaction;
        
        public ItemAddEvent(BaseItem interaction)
        {
            Interaction = interaction;
        }
    }
}