using Player;
using UnityEngine;

namespace WST.Scripts.Item
{
    public abstract class AbstractItem : AbstractPlayerRay
    {
        [field: SerializeField] public ItemSo ItemSo { get; private set; }
        protected override void RayInteraction(Transform target)
        {
            
        }

        public abstract void UseSkill();
    }
}