using a1creator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Screen = UnityEngine.Device.Screen;

namespace RegistrationNameSpace
{
    public class HintBoxView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _detailsText;

        private bool _isPointerOver;
        private bool _shouldActivate;
        private RectTransform _rect;
        
        private readonly float _tolerance = 0.1f;

        public void Start()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void Activate(string title, string details, Vector2 direction, RectTransform anchorRect = null)
        {
            _titleText.text = title;
            _detailsText.text = details;

            if (anchorRect != null)
            {
                if (_rect == null)
                {
                    _rect = GetComponent<RectTransform>();
                }
                SetAppropriateLocation(anchorRect, direction);
            }
            
            _shouldActivate = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _shouldActivate = false;
            Invoke(nameof(CheckDeactivation), _tolerance);
        }

        private void SetAppropriateLocation(RectTransform anchorRect, Vector2 direction)
        {
            Vector2 pos = (Vector2)anchorRect.position + new Vector2(direction.x * _rect.rect.width, direction.y * _rect.rect.height);
            _rect.SetPositionInsideScreen(pos);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;
            Invoke(nameof(CheckDeactivation), _tolerance);
        }

        private void CheckDeactivation()
        {
            if (!(_shouldActivate || _isPointerOver))
            {
                gameObject.SetActive(false);
            }
        }
    }
}