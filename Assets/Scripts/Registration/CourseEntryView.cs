using System;
using CourseNameSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class CourseEntryView : MonoBehaviour
    {
        public Course Course{get; private set;}
        private Toggle _toggle;
        private TextMeshProUGUI _courseNameText;
        private TextMeshProUGUI _detailsText;
        
        public void Initialize(Course course)
        {
            Course = course;
            _toggle = GetComponentInChildren<Toggle>();
            _courseNameText = transform.Find("CourseNameText").GetComponent<TextMeshProUGUI>();
            _detailsText = transform.Find("DetailsText").GetComponent<TextMeshProUGUI>();

            _courseNameText.text = course.CourseName;
            _detailsText.text = GetCourseDetails(course);
        }

        public void SetSelected(bool selected)
        {
            _toggle.isOn = selected;
        }

        private string GetCourseDetails(Course course)
        {
            return "credits: " + course.Credits + "\t" + "quota: " + course.CurrentQuota + "/" + course.Quota;
        }
    }
}