using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RegistrationNameSpace
{
    public class ClockView: MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI _clockText;
        
        private TimeManager _timeManager;
        private RectTransform _rectTransform;
        private Vector2 _dragStartPosition;

        public void Initialize(TimeManager timeManager)
        {
            _timeManager = timeManager;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _clockText.text = ConvertTimeToText(_timeManager.GetCurrentTime());
        }

        private string ConvertTimeToText(double time)
        {
            int hour = (int)(time / 3600) % 24;
            int minute = (int)(time / 60) % 60;
            int second = (int)(time) % 60;
            
            return hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 offset = eventData.position - _dragStartPosition;
            _dragStartPosition = eventData.position;
            _rectTransform.anchoredPosition += offset;
        }
    }
}