using System;
using System.Collections.Generic;
using CourseNameSpace;
using GameInfoSpace;
using UnityEngine;

namespace RegistrationNameSpace
{
    public abstract class RegistrationManagerBase
    {
        public List<Course> CourseList { get; protected set; }

        public virtual event EventHandler<TryRegisterEventArgs> RaiseTryRegisterEvent;
        
        public List<Course> RegisteredCourses { get; protected set; }
        public int SelectedIdx { get; protected set; }
        private IGameInfo _gameInfo;

        public virtual void Initialize(IGameInfo gameInfo)
        {
            _gameInfo = gameInfo;
            CourseList = _gameInfo.CourseList;
            RegisteredCourses = new List<Course>();
            SelectedIdx = -1;
        }

        public void TryRegister(int idx)
        {
            RegistrationResultType result = GetRegistrationResult(idx);

            if (result == RegistrationResultType.SUCCESS)
            {
                RegisterCourse(CourseList[idx]);
            }
            RaiseTryRegisterEvent?.Invoke(this, new TryRegisterEventArgs(result));
        }

        protected virtual RegistrationResultType GetRegistrationResult(int idx)
        {
            if (idx == -1)
            {
                return RegistrationResultType.FAILURE_COURSE_NOT_SELECTED;
            }
            
            Course course = CourseList[idx];
            
            if (GetTotalCredits() + course.Credits > _gameInfo.MaxCredits)
            {
                return RegistrationResultType.FAILURE_MAXIMUM_CREDIT_EXCEEDED;
            }

            if (DoesTimetableOverlap(course))
            {
                return RegistrationResultType.FAILURE_TIMETABLE_OVERLAP;
            }

            if (DoesCourseIDOverlap(course))
            {
                return RegistrationResultType.FAILURE_COURSE_ID_OVERLAP;
            }
            
            return RegistrationResultType.SUCCESS;
        }
        
        protected void RegisterCourse(Course course)
        {
            RegisteredCourses.Add(course);
            SelectedIdx = -1;
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

        private bool DoesTimetableOverlap(Course course)
        {
            if (course.Timetable == null)
            {
                return false;
            }
            foreach (Course otherCourse in RegisteredCourses)
            {
                if (otherCourse.Timetable == null)
                {
                    return false;
                }
                foreach (TimetableEntry timeTableEntry in course.Timetable)
                {
                    foreach (TimetableEntry otherTimeTableEntry in otherCourse.Timetable)
                    {
                        if (timeTableEntry.Day != otherTimeTableEntry.Day)
                        {
                            continue;
                        }
                        
                        int startHourMinute = timeTableEntry.StartHour * 60 + timeTableEntry.StartMinute;
                        int endHourMinute = timeTableEntry.EndHour * 60 + timeTableEntry.EndMinute;
                        int otherStartHourMinute = otherTimeTableEntry.StartHour * 60 + otherTimeTableEntry.StartMinute;
                        int otherEndHourMinute = otherTimeTableEntry.EndHour * 60 + otherTimeTableEntry.EndMinute;

                        if (startHourMinute > otherStartHourMinute && startHourMinute < otherEndHourMinute)
                        {
                            return true;
                        }

                        if (otherStartHourMinute > startHourMinute && otherStartHourMinute < endHourMinute)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool DoesCourseIDOverlap(Course course)
        {
            foreach (Course otherCourse in RegisteredCourses)
            {
                if (course.CourseID == otherCourse.CourseID)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class TryRegisterEventArgs : EventArgs
    {
        public RegistrationResultType Result { get; private set; }
        
        public TryRegisterEventArgs(RegistrationResultType result)
        {
            Result = result;
        }
    }

    public enum RegistrationResultType
    {
        SUCCESS = 0,
        FAILURE_BEFORE_START = 1,
        FAILURE_QUOTA_EXCEEDED = 2,
        FAILURE_MAXIMUM_CREDIT_EXCEEDED = 3,
        FAILURE_COURSE_NOT_SELECTED = 4,
        FAILURE_TIMETABLE_OVERLAP = 5,
        FAILURE_COURSE_ID_OVERLAP = 6
    }
}