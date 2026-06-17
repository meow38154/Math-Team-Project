using UnityEngine;

namespace WST.Scripts.Item
{
    [CreateAssetMenu(fileName = "ItemSo", menuName = "SO/item", order = 0)]
    public class ItemSo : ScriptableObject
    {
        [field: SerializeField] public Sprite SpriteImage { get; set; }
    }
}