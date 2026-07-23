using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class PopupView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _hideButton;

        public void Initialize(RegistrationManager registrationManager)
        {
            registrationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
            _hideButton.onClick.AddListener(HideMessage);
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            switch (e.Result)
            {
                case RegistrationResultType.SUCCESS:
                    ShowMessage("registration success");
                    break;
                case RegistrationResultType.FAILURE_BEFORE_START:
                    ShowMessage("course registration has not started yet");
                    break;
                case RegistrationResultType.FAILURE_QUOTA_EXCEEDED:
                    ShowMessage("total quota exceeded");
                    break;
            }
        }
        
        private void ShowMessage(string message)
        {
            _messageText.text = message;
            gameObject.SetActive(true);
        }

        private void HideMessage()
        {
            gameObject.SetActive(false);
        }
    }
}