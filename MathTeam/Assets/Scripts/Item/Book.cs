using UI;
using UnityEngine;
using UnityEngine.Events;
using WST.Scripts.Item.Items;

namespace Item
{
    public class Book : AbstractInteraction
    {
        [Header("문제\\답1\\답2\\답3\\정답")]
        [field: SerializeField] public AudioClip Audio { get; set; }
        [field: SerializeField] public string Data { get; set; }
        [SerializeField] private UnityEvent yes;
        [SerializeField] private UnityEvent no;
        public override void PickUp()
        {
            BookManager.Instance.SetQuestion(Data, yes, no, Audio);
            Destroy(gameObject);
        }
    }
}