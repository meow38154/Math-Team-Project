using UnityEngine;
using WST.EventBus;
using WST.Events.ItemSkillEvent;

namespace WST.Scripts.Item.ItemSOs
{
    [CreateAssetMenu(fileName = "SodaItem", menuName = "SO/Item/SodaShoot", order = 0)]
    public class SodaShootItemSo : AbstractItemSo
    {
        public override void RaiseEvent()
        {
            Bus<SodaShootEvent>.Raise(new SodaShootEvent());
        }

        public override bool CanUseItem()
        {
            return true;
        }
    }
}