using UnityEngine;
using UnityEngine.UI;

namespace ReservationNameSpace
{
    public class NavigatorView: MonoBehaviour
    {
        [SerializeField] private Button _registrationButton;
        [SerializeField] private Button _reservationPanelButton;
        [SerializeField] private Button _reservedPanelButton;
        
        private NavigatorManager _navigatorManager;
        
        public void Initialize(NavigatorManager navigatorManager)
        {
            _navigatorManager = navigatorManager;
            _registrationButton.onClick.AddListener(_navigatorManager.StartRegistrationScene);
            _reservationPanelButton.onClick.AddListener(_navigatorManager.LoadReservationPanel);
            _reservedPanelButton.onClick.AddListener(_navigatorManager.LoadReservedPanel);
        }
    }
}