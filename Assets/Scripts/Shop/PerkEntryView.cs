using RegistrationNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShopNameSpace
{
    public class PerkEntryView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _perkNameText;
        [SerializeField] private TextMeshProUGUI _costText;
        private ItemEntry _item;
        
        private HintBoxView _hintBoxView;
        
        public void Initialize(ItemEntry item, HintBoxView hintBoxView)
        {
            _item = item;
            _perkNameText.text = _item.ItemData.ItemName;
            _costText.text = _item.ItemData.Cost + " points";
            _hintBoxView = hintBoxView;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hintBoxView.Activate(_item.ItemData.ItemName, _item.ItemData.ItemDescription, new Vector2(-1, 0), GetComponent<RectTransform>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hintBoxView.Deactivate();
        }
    }
}