using UnityEngine;
using WST.EventBus;
using WST.Events.ItemSkillEvent;

namespace WST.Scripts.Item.ItemSOs
{
    [CreateAssetMenu(fileName = "Item", menuName = "SO/Item/SpeedUp", order = 0)]
    public class SpeedUpItemSO : AbstractItemSo
    {
        public override void RaiseEvent()
        {
            Bus<SpeedUpEvent>.Raise(new SpeedUpEvent(3));
        }

        public override bool CanUseItem()
        {
            return true;
        }
    }
}