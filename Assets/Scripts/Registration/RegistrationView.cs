using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class RegistrationView: MonoBehaviour
    {
        [SerializeField] private Button _registerButton;
        
        private RegistrationManager _registrationManager;
        
        public void Initialize(RegistrationManager registrationManager)
        {
            _registrationManager = registrationManager;
            _registerButton.onClick.AddListener(HandleRegisterButtonClick);
        }
        
        private void HandleRegisterButtonClick()
        {
            _registrationManager.TryRegister(_registrationManager.SelectedIdx);
        }
    }
}