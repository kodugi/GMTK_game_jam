using System;
using System.Collections.Generic;
using CourseNameSpace;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private CourseView _courseView;
        [SerializeField] private RegistrationView _registrationView;
        [SerializeField] private DetailsView _detailsView;
        [SerializeField] private PopupView _popupView;
        [SerializeField] private ClockView _clockView;
        
        private RegistrationManager _registrationManager;
        private TimeManager _timeManager;
        
        private void Start()
        {
            _registrationManager = new RegistrationManager();
            _timeManager = new TimeManager();

            GameInfo gameInfo = GenerateGameInfo();
            
            _registrationManager.Initialize(GameInfo.Instance.CourseList, _timeManager);
            _timeManager.Initialize(GameInfo.Instance.TimeOffset, GameInfo.Instance.TargetTime);
            
            _courseView.Initialize(GameInfo.Instance.CourseList, _registrationManager);
            _registrationView.Initialize(_registrationManager);
            _detailsView.Initialize(_registrationManager);
            _popupView.Initialize(_registrationManager);
            _clockView.Initialize(_timeManager);
        }

        private void Update()
        {
            _timeManager.UpdateTime(Time.deltaTime);
        }

        private GameInfo GenerateGameInfo()
        {
            List<Course> courseList = new List<Course>();
            courseList.Add(new Course(0, "test1", 5, CourseType.ESSENTIAL_GE, 10, 5, 5));
            courseList.Add(new Course(0, "test2", 5, CourseType.ESSENTIAL_GE, 10, 20, 5));
            return new GameInfo(courseList, 12 * 3600 + 3 * 60 - 10, 10, 21);
        }
    }
}