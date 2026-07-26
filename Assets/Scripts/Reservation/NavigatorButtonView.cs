using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigatorButtonView : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Color _highlightColor;

    private bool _selected;

    public void OnSelect(BaseEventData eventData)
    {
        _text.color = _highlightColor;
        _text.fontStyle = FontStyles.Underline;
        _selected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _text.color = Color.black;
        _text.fontStyle = FontStyles.Normal;
        _selected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = _highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_selected)
        {
            _text.color = Color.black;
        }
        
    }
}
