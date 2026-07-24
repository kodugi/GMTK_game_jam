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
        public float averageRating;
        public List<PrerequisiteData> prerequisites;
    }

    [Serializable]
    public class PrerequisiteData
    {
        public CourseData courseData;
        public float multiplier;
    }
}