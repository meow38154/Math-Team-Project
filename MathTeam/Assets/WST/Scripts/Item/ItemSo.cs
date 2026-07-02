using UnityEngine;

namespace WST.Scripts.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "SO/Item", order = 0)]
    public class ItemSo : ScriptableObject
    {
        [field: SerializeField] public Sprite ItemSprite { get; private set; }
    }
}