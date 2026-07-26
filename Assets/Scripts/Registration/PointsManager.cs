using System;
using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using ShopNameSpace;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class PointsManager
    {
        private RegistrationManagerBase _registrationManager;
        private ResultPopupView _resultPopupView;
        
        public void Initialize(RegistrationManagerBase registrationManager, ResultPopupView resultPopupView)
        {
            _registrationManager = registrationManager;
            _resultPopupView = resultPopupView;
        }
        
        public void HandleRegistrationEnd()
        {
            RegistrationResult result = CalculatePoints(_registrationManager.RegisteredCourses);
            PersistentData.Points += result.Total;
            _resultPopupView.ShowResult(result);
        }
        
        private RegistrationResult CalculatePoints(List<Course> registeredCourses)
        {
            RegistrationResult result = new RegistrationResult();
            
            CoursePointsRecord coursePointsRecord = new CoursePointsRecord();
            foreach (Course course in registeredCourses)
            {
                coursePointsRecord.CoursePointsEntries.Add(new CoursePointsEntry(course));
            }

            foreach (ItemData itemData in PersistentData.PerkList)
            {
                coursePointsRecord = itemData.GetExtraPoints(coursePointsRecord);
            }
            
            float total = 0;
            foreach (CoursePointsEntry coursePointsEntry in coursePointsRecord.CoursePointsEntries)
            {
                Course course = coursePointsEntry.Course;
                
                float currentPoints = course.Rating * course.Credits;
                if (course.Prerequisites != null && PersistentData.TakenCourseList != null)
                {
                    foreach (PrerequisiteData prerequisite in course.Prerequisites)
                    {
                        bool containsCourse = false;
                        foreach (Course takenCourse in PersistentData.TakenCourseList)
                        {
                            if (prerequisite.courseData.courseName.Equals(takenCourse.CourseName))
                            {
                                containsCourse = true;
                            }
                        }

                        if (containsCourse)
                        {
                            currentPoints = (float)Math.Round(currentPoints * prerequisite.multiplier, 2);
                        }
                    }

                    currentPoints = (float)Math.Round(currentPoints * coursePointsEntry.Multiplier, 2);
                }

                float thisCourseTotal = currentPoints + coursePointsEntry.Extra;
                result.PointsPerCourse.Add((course, thisCourseTotal));
                total += thisCourseTotal;
            }
            
            result.ExtraPoint = coursePointsRecord.ExtraTotal;
            total += coursePointsRecord.ExtraTotal;
            result.Total = (int)total;
            result.Success = result.Total >= PersistentData.GetTargetPoints();
            return result;
        }
    }

    public class RegistrationResult
    {
        public List<(Course, float)> PointsPerCourse;
        public float ExtraPoint;
        public int Total;
        public bool Success;

        public RegistrationResult()
        {
            PointsPerCourse = new List<(Course, float)>();
            ExtraPoint = 0;
            Total = 0;
            Success = false;
        }
    }
}