using System;
using System.Collections.Generic;
using UnityEngine;

namespace CourseNameSpace
{
    [CreateAssetMenu(fileName = "CourseData", menuName = "ScriptableObjects/CourseData", order = 1)]
    public class CourseData: ScriptableObject
    {
        public int courseID;
        public string courseName;
        public int credits;
        public CourseType courseType;
        public DepartmentType department;
        public int quota;
        public float averageQuota;
        public float stdQuota;
        public float averageRating;
        public float stdRating;
        public List<PrerequisiteData> prerequisites;
        public TimetableType timetableType;

        private void Reset()
        {
            stdQuota = 0.5f;
            stdRating = 0.5f;
        }
    }

    [Serializable]
    public class PrerequisiteData
    {
        public CourseData courseData;
        public float multiplier;
    }

    public enum TimetableType
    {
        NORMAL = 0,
        SHORT = 1,
        LONG = 2
    }
}