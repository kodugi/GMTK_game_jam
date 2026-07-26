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
        private RegistrationView _registrationView;
        
        public void Initialize(RegistrationManagerBase reservationManager, CourseView courseView, RegistrationView registrationView)
        {
            _reservationManager = reservationManager;
            _courseView = courseView;
            _registrationView = registrationView;
        }

        public void StartRegistrationScene()
        {
            SceneTransitionData.ReservedCourseList = _reservationManager.RegisteredCourses;
            SceneManager.LoadScene("RegistrationScene");
        }

        public void LoadReservationPanel()
        {
            _courseView.RefreshCourseList();
            _registrationView.ToggleButtons(true);
        }

        public void LoadReservedPanel()
        {
            _courseView.RefreshCourseList(_reservationManager.RegisteredCourses.Contains);
            _registrationView.ToggleButtons(false);
        }
    }
}