using RegistrationNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopNameSpace
{
    public class SidebarPerkView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        
        private HintBoxView _hintBoxView;
        private ItemData _item;

        public void Initialize(ItemData perkData, HintBoxView hintBoxView)
        {
            _item = perkData;
            _nameText.text = perkData.ItemName;
            _hintBoxView = hintBoxView;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _hintBoxView.Activate(_item.ItemName, _item.ItemDescription, new Vector2(-1, 0), GetComponent<RectTransform>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hintBoxView.Deactivate();
        }
    }
}