using GameInfoSpace;
using TMPro;
using UnityEngine;

namespace RegistrationNameSpace
{
    public class DetailsView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _detailsText;
        
        private RegistrationManagerBase _registrationManager;
        private IGameInfo _gameInfo;

        public void Initialize(RegistrationManagerBase registrationManager, IGameInfo gameInfo)
        {
            _registrationManager = registrationManager;
            _registrationManager.RaiseTryRegisterEvent += HandleTryRegisterEvent;
            UpdateDetailsText();
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            if (e.Result == RegistrationResultType.SUCCESS)
            {
                UpdateDetailsText();
            }
        }

        private void UpdateDetailsText()
        {
            int totalCredits = _registrationManager.GetTotalCredits();
            int maxCredits = _gameInfo?.MaxCredits ?? 0;

            _detailsText.text = "total credits: " + totalCredits + "/" + "maximum credits: " + maxCredits;
        }
    }
}