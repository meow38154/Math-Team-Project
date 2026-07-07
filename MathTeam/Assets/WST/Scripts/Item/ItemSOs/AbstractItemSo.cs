using UnityEngine;

namespace WST.Scripts.Item.ItemSOs
{
    [CreateAssetMenu(fileName = "Item", menuName = "SO/Item", order = 0)]
    public abstract class AbstractItemSo : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        public abstract void RaiseEvent();
    }
}