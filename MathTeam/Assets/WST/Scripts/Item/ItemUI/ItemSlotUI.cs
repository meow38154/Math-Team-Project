using UnityEngine;
using UnityEngine.UI;
using WST.Scripts.Item.ItemSOs;

namespace WST.Scripts.Item.ItemUI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image itemImage;
        
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
                itemImage.gameObject.SetActive(false);
                background.color = new Color(255, 255, 255, 0);
                ItemSo = item;
            }
            else
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = ItemSo.ItemSprite;
                background.color = new Color(255, 255, 255, 255);
                ItemSo = item;
            }
        }

        public void Select(bool canSelect)
        {
            background.color = canSelect ? Color.red : Color.white;
        }
    }
}