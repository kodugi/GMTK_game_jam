using System.Collections;
using System.Collections.Generic;
using CourseNameSpace;
using RegistrationNameSpace;
using UnityEngine;

namespace ReservationNameSpace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private CourseView _courseView;
        [SerializeField] private RegistrationView _registrationView;
        [SerializeField] private DetailsView _detailsView;
        [SerializeField] private PopupView _popupView;
        [SerializeField] private TimetableView _timetableView;
        [SerializeField] private NavigatorView _navigatorView;
        
        private ReservationManager _reservationManager;
        private NavigatorManager _navigatorManager;
        
        private void Start()
        {
            Canvas.ForceUpdateCanvases(); 
            
            _reservationManager = new ReservationManager();
            _navigatorManager = new NavigatorManager();

            List<Course> courseList = GenerateCourseList();
            ReservationInfo reservationInfo = new ReservationInfo(courseList, 21);
            
            _reservationManager.Initialize(reservationInfo);
            _navigatorManager.Initialize(_reservationManager, _courseView);
            
            _courseView.Initialize(courseList, _reservationManager);
            _registrationView.Initialize(_reservationManager);
            _detailsView.Initialize(_reservationManager, reservationInfo);
            _popupView.Initialize(_reservationManager);
            _timetableView.Initialize(_reservationManager);
            _navigatorView.Initialize(_navigatorManager);
        }

        private List<Course> GenerateCourseList()
        {
            List<Course> courseList = new List<Course>();
            List<TimetableEntry> timetable1 = new List<TimetableEntry>();
            List<TimetableEntry> timetable2 = new List<TimetableEntry>();
            timetable1.Add(new TimetableEntry(0, 9, 30, 10, 0));
            timetable2.Add(new TimetableEntry(0, 11, 0, 13, 0));
            courseList.Add(new Course(0, "test1", 5, CourseType.ESSENTIAL_GE, DepartmentType.NONE, 10, 5, 5, timetable1));
            courseList.Add(new Course(1, "test2", 5, CourseType.ESSENTIAL_GE, DepartmentType.NONE, 10, 20, 5, timetable2));
            return courseList;
        }
    }
}