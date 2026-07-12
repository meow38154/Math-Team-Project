using UnityEngine;

namespace UI
{
    public class ButtonClickEvent : MonoBehaviour
    {
        [SerializeField] private int num;

        public void Event()
        {
            BookManager.Instance.Dab(num);
        }
    }
}