using TMPro;
using UnityEngine;

namespace ShopNameSpace
{
    public class ItemEntryView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        private ItemEntry _item;
        
        public void Initialize(ItemEntry item)
        {
            _item = item;
            _itemNameText.text = _item.ItemData.ItemName;
            _itemDescriptionText.text = _item.ItemData.ItemDescription;
        }
    }
}