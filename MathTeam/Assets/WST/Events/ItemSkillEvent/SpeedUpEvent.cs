using WST.EventBus;

namespace WST.Events.ItemSkillEvent
{
    public struct SpeedUpEvent : IEvent
    {
        public float Acceleration;

        public SpeedUpEvent(float acceleration)
        {
            Acceleration = acceleration;
        }
    }
}