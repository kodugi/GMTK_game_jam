using UnityEngine.SceneManagement;

namespace RegistrationNameSpace
{
    public class SceneTransitionManager
    {
        private PointsManager _pointsManager;
        
        public void Initialize(PointsManager pointsManager)
        {
            _pointsManager = pointsManager;
        }

        public void FinishRegistration()
        {
            _pointsManager.HandleRegistrationEnd();
        }
    }
}