using GameInfoSpace;
using PersistentDataNameSpace;
using RegistrationNameSpace;
using UnityEngine.SceneManagement;

namespace ReservationNameSpace
{
    public class NavigatorManager
    {
        private RegistrationManagerBase _reservationManager;
        private CourseView _courseView;
        
        public void Initialize(RegistrationManagerBase reservationManager, CourseView courseView)
        {
            _reservationManager = reservationManager;
            _courseView = courseView;
        }

        public void StartRegistrationScene()
        {
            SceneTransitionData.ReservedCourseList = _reservationManager.RegisteredCourses;
            SceneManager.LoadScene("RegistrationScene");
        }

        public void LoadReservationPanel()
        {
            _courseView.RefreshCourseList();
        }

        public void LoadReservedPanel()
        {
            _courseView.RefreshCourseList(_reservationManager.RegisteredCourses.Contains);
        }
    }
}