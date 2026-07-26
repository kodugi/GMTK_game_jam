using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class FinishRegistrationButtonView: MonoBehaviour
    {
        public void Initialize(SceneTransitionManager sceneTransitionManager)
        {
            GetComponent<Button>().onClick.AddListener(sceneTransitionManager.FinishRegistration);
        }
    }
}