using UnityEngine;
using UnityEngine.UI;

namespace WST.Scripts.Item.UI
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
            _image.color = sprite == null ? Color.azure : Color.red;
            
            _image.sprite = sprite;
        }
    }
}