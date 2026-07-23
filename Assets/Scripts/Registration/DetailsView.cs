using TMPro;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class DetailsView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _detailsText;
        
        private RegistrationManager _registrationManager;

        public void Initialize(RegistrationManager registrationManager)
        {
            _registrationManager = registrationManager;
            _registrationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
            UpdateDetailsText();
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            if (e.Success)
            {
                UpdateDetailsText();
            }
        }

        private void UpdateDetailsText()
        {
            int totalCredits = _registrationManager.GetTotalCredits();
            int maxCredits = GameInfo.Instance?.MaxCredits ?? 0;

            _detailsText.text = "total credits: " + totalCredits + "/" + "maximum credits: " + maxCredits;
        }
    }
}