using System;
using System.Collections.Generic;
using CourseNameSpace;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class RegistrationManager
    {
        public List<Course> CourseList { get; private set; }
        private List<List<double>> _courseRegistrationTimeList;
        
        private TimeManager _timeManager;

        private readonly double GAMMA = 0.1;
        private readonly double MU = -1.0;
        private readonly double SIGMA = 0.4;

        public event EventHandler<TryRegisterEventArgs> RaiseTryRegisterEvent;
        
        public List<Course> RegisteredCourses { get; private set; }
        public int SelectedIdx { get; private set; }

        public void Initialize(List<Course> courseList, TimeManager timeManager)
        {
            CourseList = courseList;
            _timeManager = timeManager;
            RegisteredCourses = new List<Course>();
            SelectedIdx = -1;
            InitializeCourseRegistrationTimeList(courseList);
        }

        private void InitializeCourseRegistrationTimeList(List<Course> courseList)
        {
            _courseRegistrationTimeList = new List<List<double>>();
            foreach (Course course in courseList)
            {
                List<double> registeredStudents = MathUtils.ExtractMultipleLogNormal(GAMMA, MU, SIGMA, course.CurrentQuota);
                _courseRegistrationTimeList.Add(registeredStudents);
            }
        }

        public void TryRegister(int idx)
        {
            if (idx == -1)
            {
                return;
            }
            
            if (!_timeManager.IsPastTime())
            {
                RaiseTryRegisterEvent?.Invoke(this, new TryRegisterEventArgs(false, RegistrationResultType.FAILURE_BEFORE_START));
                return;
            }
            
            Course course = CourseList[idx];
            double time = _timeManager.GetPastTime();
            if (GetRegisteredQuota(idx, time) >= course.Quota)
            {
                RaiseTryRegisterEvent?.Invoke(this, new TryRegisterEventArgs(false, RegistrationResultType.FAILURE_QUOTA_EXCEEDED));
                return;
            }

            RegisterCourse(course);
            RaiseTryRegisterEvent?.Invoke(this, new TryRegisterEventArgs(true, RegistrationResultType.SUCCESS));
        }

        private void RegisterCourse(Course course)
        {
            RegisteredCourses.Add(course);
            SelectedIdx = -1;
        }

        private int GetRegisteredQuota(int idx, double elapsedTime)
        {
            List<double> registeredStudents = _courseRegistrationTimeList[idx];
            int cnt = 0;
            foreach (double d in registeredStudents)
            {
                if (d < elapsedTime)
                {
                    cnt++;
                }
            }

            return cnt;
        }
        
        public void SetSelectedIdx(int idx)
        {
            SelectedIdx = idx;
        }

        public int GetTotalCredits()
        {
            int sum = 0;
            
            foreach (Course course in RegisteredCourses)
            {
                sum += course.Credits;
            }

            return sum;
        }
    }

    public class TryRegisterEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public RegistrationResultType Result { get; private set; }
        
        public TryRegisterEventArgs(bool success, RegistrationResultType result)
        {
            Success = success;
            Result = result;
        }
    }

    public enum RegistrationResultType
    {
        SUCCESS = 0,
        FAILURE_BEFORE_START = 1,
        FAILURE_QUOTA_EXCEEDED = 2
    }
}