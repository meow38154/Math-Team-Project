using UnityEngine;

namespace WST.Scripts.Item.ItemSOs
{
    public abstract class AbstractItemSo : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
        public abstract void RaiseEvent();
    }
}