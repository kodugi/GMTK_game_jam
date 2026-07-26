using CourseNameSpace;
using PersistentDataNameSpace;
using UnityEngine.SceneManagement;

namespace RegistrationNameSpace
{
    public class SceneTransitionManager
    {
        private RegistrationManagerBase _registrationManagerBase;
        private PointsManager _pointsManager;
        private ClockView _clockView;
        
        public void Initialize(PointsManager pointsManager, RegistrationManagerBase registrationManagerBase, ClockView clockView)
        {
            _pointsManager = pointsManager;
            _registrationManagerBase = registrationManagerBase;
            _clockView = clockView;
        }

        public void FinishRegistration()
        {
            _pointsManager.HandleRegistrationEnd();
            _clockView.gameObject.SetActive(false);
        }

        public void GameOver()
        {
            PersistentData.InitializePersistentData();
            SceneManager.LoadScene("StartScene");
        }

        public void NextRound()
        {
            PersistentData.Round++;
            foreach (Course course in _registrationManagerBase.RegisteredCourses)
            {
                PersistentData.TakenCourseList.Add(course);
            }
            SceneManager.LoadScene("Shop");
        }
    }
}