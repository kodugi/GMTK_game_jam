using System;
using System.Collections.Generic;
using RegistrationNameSpace;
using UnityEngine;
using Random = System.Random;

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
        
        public string GetDepartmentName()
        {
            switch (Department)
            {
                case DepartmentType.HUMAN_LITERATURE:
                    return "College of Human Literature";
                case DepartmentType.NATURAL_SCIENCES:
                    return "College of Natural Sciences";
                case DepartmentType.SOCIAL_SCIENCES:
                    return "College of Social Sciences";
                case DepartmentType.EDUCATION:
                    return "College of Education";
                case DepartmentType.ENGINEERING:
                    return "College of Engineering";
                default:
                    return "College of Liberal Arts";
            }
        }

        public string GetCourseDetails()
        {
            string result = "Department: " + GetDepartmentName() + "\n" + "Average Ratings: " + Rating.ToString("F2") + "\n" + "\nPrerequisites";
            if (Prerequisites == null || Prerequisites.Count == 0)
            {
                result += ": None";
                return result;
            }
            
            for (int i = 0; i < Prerequisites.Count; i++)
            {
                result += "\n";
                result += Prerequisites[i].courseData.courseName + ": X" + Prerequisites[i].multiplier;
            }

            return result;
        }

        public static Course FromCourseData(CourseData courseData)
        {
            return new Course(
                courseData.courseID,
                courseData.courseName,
                courseData.credits,
                courseData.courseType,
                courseData.department,
                courseData.quota,
                GenerateCurrentQuota(courseData.averageQuota),
                GenerateAverageRatings(courseData.averageRating),
                GenerateTimetable(courseData.timetableType),
                courseData.prerequisites
            );
        }

        private static int GenerateCurrentQuota(float averageQuota)
        {
            return (int)Math.Clamp(MathUtils.ExtractNormal(averageQuota, 1), 0, 1000);
        }

        private static float GenerateAverageRatings(float averageRating)
        {
            return (float)Math.Clamp(MathUtils.ExtractNormal(averageRating, 1), 0, 5);
        }

        private static List<TimetableEntry> GenerateTimetable(TimetableType timetableType)
        {
            int baseStart = 540, baseEnd = 1260;
            int entryCount, entryDuration, timetableGap, firstStart, lastStart;

            switch (timetableType)
            {
                case TimetableType.SHORT:
                    entryCount = 1;
                    entryDuration = 50;
                    timetableGap = 60;
                    firstStart = baseStart + 60;
                    lastStart = 1020;
                    break;
                case TimetableType.LONG:
                    entryCount = 1;
                    entryDuration = 110;
                    timetableGap = 120;
                    firstStart = baseStart;
                    lastStart = 1020;
                    break;
                default:
                    entryCount = 2;
                    entryDuration = 75;
                    timetableGap = 90;
                    firstStart = baseStart + 30;
                    lastStart = 1050;
                    break;
            }

            int interval = (lastStart - firstStart + 1) / timetableGap;
            Random random = new Random();
            int selectedInterval = random.Next(0, interval);
            int startTime = firstStart + selectedInterval * timetableGap;
            int endTime = startTime + entryDuration;
            
            int selectedDay;
            List<TimetableEntry> timetableEntries = new List<TimetableEntry>();
            if (timetableType == TimetableType.NORMAL)
            {
                selectedDay = random.Next(0, 2);
            }
            else
            {
                selectedDay = random.Next(0, 5);
            }

            TimetableEntry selectedTimetableEntry =
                new TimetableEntry(selectedDay, startTime / 60, startTime % 60, endTime / 60, endTime % 60);
            timetableEntries.Add(selectedTimetableEntry);

            if (timetableType == TimetableType.NORMAL)
            {
                TimetableEntry selectedTimetableEntry2 =
                    new TimetableEntry(selectedDay + 2, startTime / 60, startTime % 60, endTime / 60, endTime % 60);
                timetableEntries.Add(selectedTimetableEntry2);
            }
            
            return timetableEntries;
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
        LIBERAL_ARTS = 0,
        HUMAN_LITERATURE = 1,
        NATURAL_SCIENCES = 2,
        SOCIAL_SCIENCES = 3,
        EDUCATION = 4,
        ENGINEERING = 5
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