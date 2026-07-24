using TMPro;
using UnityEngine;

namespace ShopNameSpace
{
    public class DetailsView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsText;
        
        private WalletManager _walletManager;

        public void Initialize(WalletManager walletManager)
        {
            _walletManager = walletManager;
            SetPointsText(_walletManager.Points);
        }

        public void SetPointsText(int points)
        {
            _pointsText.text = points.ToString();
        }
    }
}