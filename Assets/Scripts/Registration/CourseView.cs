using System;
using System.Collections.Generic;
using CourseNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class CourseView: MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _courseEntryPrefab;

        private List<GameObject> _courseEntries;
        private RegistrationManagerBase _registrationManager;

        public void Initialize(List<Course> courseList, RegistrationManagerBase registrationManager)
        {
            _registrationManager = registrationManager;
            _registrationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
            SpawnCourseEntries(courseList);
        }

        public void RefreshCourseList()
        {
            SpawnCourseEntries(_registrationManager.CourseList);
        }
        
        public void RefreshCourseList(Func<Course, bool> filter)
        {
            SpawnCourseEntries(_registrationManager.CourseList, filter);
        }

        private void SpawnCourseEntries(List<Course> courseList, Func<Course, bool> filter)
        {
            if (_courseEntries != null && _courseEntries.Count > 0)
            {
                foreach (GameObject courseEntry in _courseEntries)
                {
                    GameObject.Destroy(courseEntry);
                }
            }
            
            _courseEntries = new List<GameObject>();
            for (int i = 0; i < courseList.Count; i++)
            {
                int idx = i;
                Course course = courseList[i];
                if (!filter(course))
                {
                    _courseEntries.Add(null);
                    continue;
                }
                GameObject courseEntry = Instantiate(_courseEntryPrefab, _content.transform);
                courseEntry.GetComponent<CourseEntryView>().Initialize(course);
                courseEntry.GetComponentInChildren<Toggle>().onValueChanged.AddListener((bool toggled) => HandleCourseEntryClick(idx, toggled));
                _courseEntries.Add(courseEntry);
            }
        }
        
        private void SpawnCourseEntries(List<Course> courseList)
        {
            SpawnCourseEntries(courseList, (Course c) => !_registrationManager.RegisteredCourses.Contains(c));
        }

        private void HandleCourseEntryClick(int idx, bool toggled)
        {
            if (toggled)
            {
                if (_registrationManager.SelectedIdx != -1)
                {
                    _courseEntries[_registrationManager.SelectedIdx].GetComponent<CourseEntryView>().SetSelected(false);
                }
                _registrationManager.SetSelectedIdx(idx);
                _courseEntries[idx].GetComponent<CourseEntryView>().SetSelected(true);
            }
            else
            {
                if (_registrationManager.SelectedIdx == idx)
                {
                    _registrationManager.SetSelectedIdx(-1);
                    _courseEntries[idx].GetComponent<CourseEntryView>().SetSelected(false);
                }
            }
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            if (e.Result == RegistrationResultType.SUCCESS)
            {
                RefreshCourseList();
            }
        }
    }
}