using System;
using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;

namespace RegistrationNameSpace
{
    public class PointsManager
    {
        public void HandleRegistrationEnd()
        {
            
        }
        
        private int CalculatePoints(List<Course> registeredCourses)
        {
            float total = 0;
            foreach (Course course in registeredCourses)
            {
                float currentPoints = course.Rating * course.Credits; // TODO: might change the formula later
                if (course.Prerequisites != null && PersistentData.TakenCourseList != null)
                {
                    foreach (PrerequisiteData prerequisite in course.Prerequisites)
                    {
                        bool containsCourse = false;
                        foreach (Course takenCourse in PersistentData.TakenCourseList)
                        {
                            if (prerequisite.courseData.courseID == takenCourse.CourseID)
                            {
                                containsCourse = true;
                            }
                        }

                        if (containsCourse)
                        {
                            currentPoints *= prerequisite.multiplier;
                        }
                    }
                }

                total += currentPoints;
            }

            return (int)total;
        }
    }
}