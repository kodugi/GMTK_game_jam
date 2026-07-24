using TMPro;
using UnityEngine;

namespace ShopNameSpace
{
    public class PerkEntryView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _perkNameText;
        [SerializeField] private TextMeshProUGUI _perkDescriptionText;
        private ItemEntry _item;
        
        public void Initialize(ItemEntry item)
        {
            _item = item;
            _perkNameText.text = _item.ItemData.ItemName;
            _perkDescriptionText.text = _item.ItemData.ItemDescription;
        }
    }
}