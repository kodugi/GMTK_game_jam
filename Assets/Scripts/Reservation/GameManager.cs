using System.Collections;
using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using RegistrationNameSpace;
using UnityEngine;
using Random = System.Random;

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

        [SerializeField] private List<CourseData> _courseDataList;
        
        private ReservationManager _reservationManager;
        private NavigatorManager _navigatorManager;
        
        private void Start()
        {
            PersistentData.InitializePersistentData();
            Canvas.ForceUpdateCanvases(); 
            
            _reservationManager = new ReservationManager();
            _navigatorManager = new NavigatorManager();

            List<Course> courseList = GenerateCourseList();
            ReservationInfo reservationInfo = new ReservationInfo(courseList, PersistentData.MaxCredits);
            
            _reservationManager.Initialize(reservationInfo);
            _navigatorManager.Initialize(_reservationManager, _courseView, _registrationView);
            
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
            /*List<TimetableEntry> timetable1 = new List<TimetableEntry>();
            List<TimetableEntry> timetable2 = new List<TimetableEntry>();
            timetable1.Add(new TimetableEntry(0, 9, 30, 10, 0));
            timetable2.Add(new TimetableEntry(0, 11, 0, 13, 0));
            courseList.Add(new Course(0, "test1", 5, CourseType.ESSENTIAL_GE, DepartmentType.LIBERAL_ARTS, 10, 5, 5, timetable1));
            courseList.Add(new Course(1, "test2", 5, CourseType.ESSENTIAL_GE, DepartmentType.LIBERAL_ARTS, 10, 20, 5, timetable2));*/

            Dictionary<(CourseType, DepartmentType), List<CourseData>> courseDataDict = BuildCourseDataDict();
            Random random = new Random();
            
            for (int i = 0; i < PersistentData.CourseSlots; i++)
            {
                CourseType courseType = SelectFromCoursePool();
                DepartmentType departmentType = SelectFromDepartmentPool();
                if (courseDataDict.TryGetValue((courseType, departmentType), out List<CourseData> selectedList) && selectedList.Count > 0)
                {
                    courseList.Add(Course.FromCourseData(selectedList[random.Next(0, selectedList.Count)]));
                }
                else
                {
                    courseList.Add(Course.FromCourseData(GetFillerCourseData(courseDataDict)));
                }
            }
            return courseList;
        }

        private Dictionary<(CourseType, DepartmentType), List<CourseData>> BuildCourseDataDict()
        {
            Dictionary<(CourseType, DepartmentType), List<CourseData>> courseDataDict = new Dictionary<(CourseType, DepartmentType), List<CourseData>>();
            foreach (CourseData courseData in _courseDataList)
            {
                if (courseDataDict.TryGetValue((courseData.courseType, courseData.department),
                        out List<CourseData> courseDataList))
                {
                    courseDataList.Add(courseData);
                }
                else
                {
                    courseDataDict[(courseData.courseType, courseData.department)] = new List<CourseData>();
                    courseDataDict[(courseData.courseType, courseData.department)].Add(courseData);
                }
            }
            
            return courseDataDict;
        }

        private CourseType SelectFromCoursePool()
        {
            Random random = new Random();
            int sum = 0;
            foreach (var kvp in PersistentData.CourseTypePool)
            {
                sum += kvp.Value;
            }
                
            int idx = random.Next(0, sum);
                
            int partialSum = 0;
            foreach (var kvp in PersistentData.CourseTypePool)
            {
                partialSum += kvp.Value;
                if (partialSum > idx)
                {
                    return kvp.Key;
                }
            }
            
            Debug.LogError("something is wrong with selection");
            return CourseType.ESSENTIAL_GE;
        }
        
        private DepartmentType SelectFromDepartmentPool()
        {
            Random random = new Random();
            int sum = 0;
            foreach (var kvp in PersistentData.DepartmentPool)
            {
                sum += kvp.Value;
            }
                
            int idx = random.Next(0, sum);
                
            int partialSum = 0;
            foreach (var kvp in PersistentData.DepartmentPool)
            {
                partialSum += kvp.Value;
                if (partialSum > idx)
                {
                    return kvp.Key;
                }
            }
            
            Debug.LogError("something is wrong with selection: sum is " + sum + ", but partialSum is " + partialSum);
            return DepartmentType.LIBERAL_ARTS;
        }

        private CourseData GetFillerCourseData(Dictionary<(CourseType, DepartmentType), List<CourseData>> courseDataDict)
        {
            List<List<CourseData>> courseDataLists = new List<List<CourseData>>();
            foreach (var kvp in courseDataDict)
            {
                courseDataLists.Add(kvp.Value);
            }

            if (courseDataLists.Count == 0)
            {
                Debug.LogError("course data dict is totally empty; fill it up");
            }
            
            Random random = new Random();
            int listIdx = random.Next(0, courseDataLists.Count);
            return courseDataLists[listIdx][random.Next(0, courseDataLists[listIdx].Count)];
        }
    }
}