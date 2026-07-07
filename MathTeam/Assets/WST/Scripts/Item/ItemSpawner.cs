using System;
using UnityEngine;
using WST.Scripts.Item.Items;
using Random = UnityEngine.Random;

namespace WST.Scripts.Item
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private AbstractItem[] items;
        [SerializeField] private Transform[] spawnPoint;

        private void Awake()
        {
            SpawnItem();
        }

        private void SpawnItem()
        {
            foreach (Transform trans in spawnPoint)
            {
                int randItemNum = Random.Range(0, items.Length);
                Instantiate(items[randItemNum], trans.position, Quaternion.identity);
            }
        }
    }
}