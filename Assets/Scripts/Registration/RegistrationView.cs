using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class RegistrationView: MonoBehaviour
    {
        [SerializeField] private Button _registerButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _finishRegistrationButton;
        
        private RegistrationManagerBase _registrationManager;
        
        public void Initialize(RegistrationManagerBase registrationManager)
        {
            _registrationManager = registrationManager;
            _registerButton.onClick.AddListener(HandleRegisterButtonClick);
            _removeButton.onClick.AddListener(HandleRemoveButtonClick);
            ToggleButtons(true);
        }
        
        private void HandleRegisterButtonClick()
        {
            _registrationManager.TryRegister(_registrationManager.SelectedIdx);
        }

        private void HandleRemoveButtonClick()
        {
            _registrationManager.TryRemove(_registrationManager.SelectedIdx);
        }

        public void ToggleButtons(bool isRegisterButtonOn)
        {
            _registerButton.gameObject.SetActive(isRegisterButtonOn);
            _removeButton.gameObject.SetActive(!isRegisterButtonOn);
        }
    }
}