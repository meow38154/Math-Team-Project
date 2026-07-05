using UnityEngine;
using UnityEngine.UI;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemSlotUI : MonoBehaviour
    {
        private Image _image;
        public AbstractItemSo ItemSo {get; private set;}
        public void Init()
        {
            _image = GetComponent<Image>();
            AddItem();
        }

        public void AddItem(AbstractItemSo item = null)
        {
            if (item == null)
            {
                _image.sprite = null;
                ItemSo = item;
            }
            else
            {
                _image.sprite = ItemSo.ItemSprite;
                ItemSo = item;
            }
        }
    }
}