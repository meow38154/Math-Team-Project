using System;

namespace WST.EventBus
{
    public static class Bus<T> where T : IEvent
    {
        public static event Action<T> OnEvent;
        public static void Raise(T value) => OnEvent?.Invoke(value);
    }
}