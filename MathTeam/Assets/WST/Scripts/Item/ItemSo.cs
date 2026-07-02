using UnityEngine;
using WST.EventBus;

namespace WST.Scripts.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "SO/Item", order = 0)]
    public abstract class ItemSo : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        public abstract void RaiseEvent();
    }
}