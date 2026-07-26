using System;
using System.Collections.Generic;
using System.Linq;
using CourseNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class CourseEntryView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Course Course{ get; private set; }
        private Toggle _toggle;
        private TextMeshProUGUI _courseNameText;
        private TextMeshProUGUI _detailsText;
        private TextMeshProUGUI _timetableText;
        private HintBoxView _hintBoxView;
        
        public void Initialize(Course course, HintBoxView hintBoxView)
        {
            Course = course;
            _toggle = GetComponentInChildren<Toggle>();
            _courseNameText = transform.Find("CourseNameText").GetComponent<TextMeshProUGUI>();
            _detailsText = transform.Find("DetailsText").GetComponent<TextMeshProUGUI>();
            _timetableText = transform.Find("TimetableText").GetComponent<TextMeshProUGUI>();
            _hintBoxView = hintBoxView;

            _courseNameText.text = course.CourseName;
            _detailsText.text = GetCourseDetails(course);
            _timetableText.text = GetTimetableText(course);
        }

        public void SetSelected(bool selected)
        {
            _toggle.SetIsOnWithoutNotify(selected);
        }

        private string GetCourseDetails(Course course)
        {
            return "credits: " + course.Credits + " | " + "reserved quota: " + course.CurrentQuota + "/" + course.Quota;
        }

        private string GetTimetableText(Course course)
        {
            List<TimetableEntry> sorted = course.Timetable.OrderBy(t => t.Day).ThenBy(t => t.StartHour).ThenBy(t => t.StartMinute).ToList();
            string result = "";
            foreach (TimetableEntry t in sorted)
            {
                switch (t.Day)
                {
                    case 0:
                        result += "Mon";
                        break;
                    case 1:
                        result += "Tue";
                        break;
                    case 2:
                        result += "Wed";
                        break;
                    case 3:
                        result += "Thu";
                        break;
                    case 4:
                        result += "Fri";
                        break;
                }
                result += "(" + t.StartHour.ToString("D2") + ":" + t.StartMinute.ToString("D2") + "~" + t.EndHour.ToString("D2") + ":" + t.EndMinute.ToString("D2") + ") ";
            }

            return result;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hintBoxView?.Activate(Course.CourseName, Course.GetCourseDetails(), new Vector2(0, 0), GetComponent<RectTransform>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hintBoxView?.Deactivate();
        }
    }
}