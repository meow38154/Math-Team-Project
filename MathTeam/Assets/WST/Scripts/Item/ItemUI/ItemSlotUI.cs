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

        public void Init()
        {
            _image = GetComponent<Image>();
            AddItem(null);
        }

        public void AddItem(Sprite item)
        {
            if (item == null)
            {
                itemImage.gameObject.SetActive(false);
                background.color = new Color(255, 255, 255, 0);
            }
            else
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = item;
                background.color = new Color(255, 255, 255, 255);
            }
        }

        public void Select(bool canSelect)
        {
            background.color = canSelect ? Color.red : Color.white;
        }
    }
}