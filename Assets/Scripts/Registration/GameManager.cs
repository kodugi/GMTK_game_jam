using System;
using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using UnityEngine;
using Random = System.Random;

namespace RegistrationNameSpace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private CourseView _courseView;
        [SerializeField] private RegistrationView _registrationView;
        [SerializeField] private DetailsView _detailsView;
        [SerializeField] private PopupView _popupView;
        [SerializeField] private ClockView _clockView;
        [SerializeField] private FinishRegistrationButtonView _finishRegistrationButtonView;
        [SerializeField] private ResultPopupView _resultPopupView;
        
        private RegistrationManager _registrationManager;
        private TimeManager _timeManager;
        private PointsManager _pointsManager;
        private SceneTransitionManager _sceneTransitionManager;
        
        private void Start()
        {
            _registrationManager = new RegistrationManager();
            _timeManager = new TimeManager();
            _pointsManager = new PointsManager();
            _sceneTransitionManager = new SceneTransitionManager();

            GameInfo gameInfo;
            
            if (SceneTransitionData.ReservedCourseList == null)
            {
                gameInfo = GenerateGameInfo();
            }
            else
            {
                Random random = new Random();
                int targetTime = random.Next(5, 20);
                gameInfo = new GameInfo(SceneTransitionData.ReservedCourseList, 8 * 3600 + 30 * 60 - targetTime, targetTime, PersistentData.MaxCredits);
            }
            
            _registrationManager.Initialize(gameInfo, _timeManager);
            _timeManager.Initialize(gameInfo.TimeOffset, gameInfo.TargetTime);
            _pointsManager.Initialize(_registrationManager, _resultPopupView);
            _sceneTransitionManager.Initialize(_pointsManager);
            
            _courseView.Initialize(gameInfo.CourseList, _registrationManager);
            _registrationView.Initialize(_registrationManager);
            _detailsView.Initialize(_registrationManager, gameInfo);
            _popupView.Initialize(_registrationManager);
            _clockView.Initialize(_timeManager);
            _finishRegistrationButtonView.Initialize(_sceneTransitionManager);
            _resultPopupView.Initialize();
        }

        private void Update()
        {
            _timeManager.UpdateTime(Time.deltaTime);
        }

        private GameInfo GenerateGameInfo()
        {
            List<Course> courseList = new List<Course>();
            courseList.Add(new Course(0, "test1", 5, CourseType.ESSENTIAL_GE, DepartmentType.LIBERAL_ARTS, 10, 5, 5));
            courseList.Add(new Course(0, "test2", 5, CourseType.ESSENTIAL_GE, DepartmentType.LIBERAL_ARTS, 10, 20, 5));
            return new GameInfo(courseList, 8 * 3600 + 30 * 60 - 10, 10, 21);
        }
    }
}