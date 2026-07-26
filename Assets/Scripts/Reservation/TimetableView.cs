using System.Collections.Generic;
using CourseNameSpace;
using RegistrationNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReservationNameSpace
{
    public class TimetableView: MonoBehaviour
    {
        [SerializeField] private GameObject _timetableColumn;
        [SerializeField] private GameObject _timetableCell;
        [SerializeField] private GameObject _timetableColumnGuide;
        [SerializeField] private GameObject _timetableGuideLabel;
        
        private RegistrationManagerBase _reservationManager;
        private RectTransform _rectTransform;

        private readonly string[] _dayName = new string[5] {"Mon", "Tue", "Wed", "Thu", "Fri"};
        private readonly int _startHourMinute = 540;
        private readonly int _endHourMinute = 1260;

        private List<GameObject> _timetableColumnViewports;

        public void Initialize(RegistrationManagerBase reservationManager)
        {
            _rectTransform = GetComponent<RectTransform>();
            _reservationManager = reservationManager;
            _reservationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
            _reservationManager.RaiseTryRemoveEvent += HandleTryRemoveEvent;
            
            RectTransform timetableColumnGuideRect = _timetableColumnGuide.GetComponent<RectTransform>();
            
            for (int i = _startHourMinute / 60; i < _endHourMinute / 60; i++)
            {
                float startPos = GetPositionOnViewport(i, 0, timetableColumnGuideRect);
                GameObject timetableGuideLabel = Instantiate(_timetableGuideLabel, _timetableColumnGuide.transform);
                timetableGuideLabel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -startPos);
                TextMeshProUGUI text = timetableGuideLabel.GetComponent<TextMeshProUGUI>();
                text.text = i.ToString();
                text.color = Color.black;
            }
            
            _timetableColumnViewports = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                GameObject timetableColumn = Instantiate(_timetableColumn, transform);
                timetableColumn.GetComponentInChildren<TextMeshProUGUI>().text = _dayName[i];
                timetableColumn.GetComponent<RectTransform>().sizeDelta = new Vector2(_rectTransform.rect.width / 5, _rectTransform.rect.height);
                _timetableColumnViewports.Add(timetableColumn.transform.Find("TimetableColumnViewport").gameObject);
            }
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            if (e.Result != RegistrationResultType.SUCCESS)
            {
                return;
            }

            UpdateTimetable();
        }
        
        private void HandleTryRemoveEvent(object sender, TryRemoveEventArgs e)
        {
            if (e.Result != RemoveResultType.SUCCESS)
            {
                return;
            }

            UpdateTimetable();
        }

        private void UpdateTimetable()
        {
            foreach (GameObject timetableColumnViewport in _timetableColumnViewports)
            {
                for (int i = timetableColumnViewport.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(timetableColumnViewport.transform.GetChild(i).gameObject);
                }
            }
            
            List<Course> registeredCourses = _reservationManager.RegisteredCourses;
            foreach (Course course in registeredCourses)
            {
                Color color = Random.ColorHSV();
                foreach (TimetableEntry timetableEntry in course.Timetable)
                {
                    GameObject timetableColumnViewport = _timetableColumnViewports[timetableEntry.Day];
                    RectTransform rectTransform = timetableColumnViewport.GetComponent<RectTransform>();
                    GameObject timetableCell = Instantiate(_timetableCell, timetableColumnViewport.transform);
                    float startPos = GetPositionOnViewport(timetableEntry.StartHour, timetableEntry.StartMinute, rectTransform);
                    float endPos = GetPositionOnViewport(timetableEntry.EndHour, timetableEntry.EndMinute, rectTransform);
                    
                    timetableCell.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -startPos);
                    timetableCell.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.rect.width, endPos - startPos);
                    timetableCell.GetComponent<Image>().color = color;
                }
            }
        }

        private float GetPositionOnViewport(int hour, int minute, RectTransform timetableColumnViewportRect)
        {
            int hourMinute = hour * 60 + minute;
            float ratio = (float)(hourMinute - _startHourMinute) / (float)(_endHourMinute - _startHourMinute);
            return timetableColumnViewportRect.rect.height * ratio;
        }
    }
}