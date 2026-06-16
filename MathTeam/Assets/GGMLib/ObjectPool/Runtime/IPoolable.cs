using UnityEngine;

namespace GGMLib.ObjectPool.Runtime
{
    public interface IPoolable
    {
        PoolItemSO PoolItem { get; }
        GameObject GameObject { get; }
        
        void ResetItem();
    }
}