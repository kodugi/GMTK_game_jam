using System;
using CourseNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class CourseEntryView : MonoBehaviour
    {
        public Course Course{get; private set;}
        private Toggle _toggle;
        
        public void Initialize(Course course)
        {
            Course = course;
            _toggle = GetComponentInChildren<Toggle>();
        }

        public void SetSelected(bool selected)
        {
            _toggle.isOn = selected;
        }
    }
}