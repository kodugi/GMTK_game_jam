using System;
using System.Collections.Generic;
using CourseNameSpace;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class RegistrationManager: RegistrationManagerBase
    {
        private List<List<double>> _courseRegistrationTimeList;
        
        private TimeManager _timeManager;

        private readonly double GAMMA = 0.1;
        private readonly double MU = 0;
        private readonly double SIGMA = 0.4;

        public void Initialize(GameInfo gameInfo, TimeManager timeManager)
        {
            base.Initialize(gameInfo);
            _timeManager = timeManager;
            InitializeCourseRegistrationTimeList(CourseList);
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

        protected override RegistrationResultType GetRegistrationResult(int idx)
        {
            if (!_timeManager.IsPastTime())
            {
                return RegistrationResultType.FAILURE_BEFORE_START;
            }

            RegistrationResultType result = base.GetRegistrationResult(idx);
            if (result != RegistrationResultType.SUCCESS)
            {
                return result;
            }
            
            Course course = CourseList[idx];
            double time = _timeManager.GetPastTime();
            if (GetRegisteredQuota(idx, time) >= course.Quota)
            {
                return RegistrationResultType.FAILURE_QUOTA_EXCEEDED;
            }
            
            return RegistrationResultType.SUCCESS;
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
    }
}