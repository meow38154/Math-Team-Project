using UnityEngine;
using WST.EventBus;

namespace WST.Events.ItemSkillEvent
{
    public struct SpeedUpEvent : IEvent
    {
        public float DecelerationAmount;

        public SpeedUpEvent(float decelerationAmount)
        {
            DecelerationAmount = decelerationAmount;
        }
    }
}