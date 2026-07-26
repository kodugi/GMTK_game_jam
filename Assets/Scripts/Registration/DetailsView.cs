using GameInfoSpace;
using PersistentDataNameSpace;
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
            _registrationManager.RaiseTryRemoveEvent += HandleTryRemoveEvent;
            UpdateDetailsText();
        }

        private void HandleTryRegisterEvent(object sender, TryRegisterEventArgs e)
        {
            if (e.Result == RegistrationResultType.SUCCESS)
            {
                UpdateDetailsText();
            }
        }

        private void HandleTryRemoveEvent(object sender, TryRemoveEventArgs e)
        {
            if (e.Result == RemoveResultType.SUCCESS)
            {
                UpdateDetailsText();
            }
        }

        private void UpdateDetailsText()
        {
            int totalCredits = _registrationManager.GetTotalCredits();
            int maxCredits = PersistentData.MaxCredits;

            _detailsText.text = "total credits: " + totalCredits + " / " + "maximum credits: " + maxCredits;
        }
    }
}