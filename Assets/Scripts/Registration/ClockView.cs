using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RegistrationNameSpace
{
    public class ClockView: MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private TextMeshProUGUI _clockText;
        [SerializeField] private TextMeshProUGUI _remainingTimeText;
        
        private TimeManager _timeManager;
        private RectTransform _rectTransform;
        private Vector2 _dragStartPosition;

        public void Initialize(TimeManager timeManager)
        {
            _timeManager = timeManager;
            _rectTransform = GetComponent<RectTransform>();
            _remainingTimeText.color = Color.green;
        }

        private void Update()
        {
            _clockText.text = ConvertTimeToText(_timeManager.GetCurrentTime());
            if (_timeManager.IsPastTime())
            {
                _remainingTimeText.text = "Remaining time\n00:00:00";
                _remainingTimeText.color = Color.red;
            }
            else
            {
                _remainingTimeText.text = "Remaining time\n" + ConvertTimeToText(_timeManager.GetRemainingTime());
            }
        }

        private string ConvertTimeToText(double time)
        {
            double rounded = Math.Round(time);
            int hour = (int)(rounded / 3600) % 24;
            int minute = (int)(rounded / 60) % 60;
            int second = (int)Math.Round((rounded)) % 60;
            
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