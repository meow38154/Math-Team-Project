using UnityEngine;

namespace GGMLib.ObjectPool.Runtime
{
    [CreateAssetMenu(fileName = "Pool Item", menuName = "Lib/ObjectPool/PoolItem")]
    public class PoolItemSO : ScriptableObject
    {
        public string poolName;
        public GameObject prefab;
        public int initCount;
    }
}