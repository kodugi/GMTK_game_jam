using System;
using System.Collections.Generic;
using UnityEngine;

namespace CourseNameSpace
{
    public class Course
    {
        public int CourseID { get; private set; }
        public string CourseName { get; private set; }
        public int Credits { get; private set; }
        public CourseType CourseType { get; private set; }
        public DepartmentType Department { get; private set; }
        public int Quota { get; private set; }
        public int CurrentQuota { get; private set; }
        public float Rating { get; private set; }
        public List<TimetableEntry> Timetable { get; private set; }
        public List<PrerequisiteData> Prerequisites { get; private set; }

        public Course(int courseID, string courseName, int credits, CourseType courseType, DepartmentType department, int quota, int currentQuota, float rating, List<TimetableEntry> timetable = null, List<PrerequisiteData> prerequisites = null)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            CourseType = courseType;
            Department = department;
            Quota = quota;
            CurrentQuota = currentQuota;
            Rating = rating;
            Timetable = timetable;
            Prerequisites = prerequisites;
        }
    }

    public enum CourseType
    {
        ESSENTIAL_GE = 0,
        NON_ESSENTIAL_GE = 1,
        ESSENTIAL_MAJOR = 2,
        NON_ESSENTIAL_MAJOR = 3
    }

    public enum DepartmentType
    {
        NONE = 0
    }
    
    [Serializable]
    public class TimetableEntry
    {
        public int Day { get; private set; } // 0~4, Mon~Fri
        public int StartHour { get; private set; } // 0~23
        public int StartMinute { get; private set; } // 0~59
        public int EndHour { get; private set; }
        public int EndMinute { get; private set; }

        public TimetableEntry(int day, int startHour, int startMinute, int endHour, int endMinute)
        {
            Day = day;
            StartHour = startHour;
            StartMinute = startMinute;
            EndHour = endHour;
            EndMinute = endMinute;
        }
    }
}