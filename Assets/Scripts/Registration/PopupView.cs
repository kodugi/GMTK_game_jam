using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class PopupView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _hideButton;

        public void Initialize(RegistrationManagerBase registrationManager)
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
                case RegistrationResultType.FAILURE_MAXIMUM_CREDIT_EXCEEDED:
                    ShowMessage("maximum credits exceeded");
                    break;
                case RegistrationResultType.FAILURE_COURSE_NOT_SELECTED:
                    ShowMessage("please select a course");
                    break;
                case RegistrationResultType.FAILURE_TIMETABLE_OVERLAP:
                    ShowMessage("timetable overlap");
                    break;
                case RegistrationResultType.FAILURE_COURSE_ID_OVERLAP:
                    ShowMessage("course id overlap");
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