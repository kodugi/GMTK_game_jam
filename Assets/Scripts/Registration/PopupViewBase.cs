using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RegistrationNameSpace
{
    public class PopupViewBase: MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _messageText;
        [SerializeField] protected Button _hideButton;

        public virtual void Initialize()
        {
            _hideButton.onClick.AddListener(HideMessage);
        }
        
        protected void ShowMessage(string message)
        {
            _messageText.text = message;
            gameObject.SetActive(true);
        }

        protected void HideMessage()
        {
            gameObject.SetActive(false);
        }
    }
}