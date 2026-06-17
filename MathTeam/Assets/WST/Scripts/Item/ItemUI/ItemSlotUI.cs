using UnityEngine;
using UnityEngine.UI;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemSlotUI : MonoBehaviour
    {
        private Image _image;

        public void Init()
        {
            _image = GetComponent<Image>();
            Show(null);
        }

        public void Show(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}