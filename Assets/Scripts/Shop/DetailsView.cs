using System.Collections;
using TMPro;
using UnityEngine;

namespace ShopNameSpace
{
    public class DetailsView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _detailsText;

        private ItemManager _itemManager;
        private WalletManager _walletManager;

        private readonly float _msgDuration = 1f;

        public void Initialize(ItemManager itemManager, WalletManager walletManager)
        {
            _itemManager = itemManager;
            _walletManager = walletManager;
            SetPointsText();
            _itemManager.RaiseTryPurchaseEvent += HandleTryPurchaseEvent;
        }

        public void SetPointsText()
        {
            _detailsText.text = "Current points: " + _walletManager.Points;
        }

        IEnumerator SetDetailsText(string msg)
        {
            _detailsText.text = msg;
            yield return new WaitForSeconds(_msgDuration);
            SetPointsText();
        }

        private void HandleTryPurchaseEvent(object sender, TryPurchaseEventArgs e)
        {
            switch (e.Result)
            {
                case PurchaseResultType.SUCCESS:
                    if (e.Item.ItemData.ItemType == ItemType.Item)
                    {
                        StartCoroutine(SetDetailsText("Posted an article!"));
                    }
                    else
                    {
                        StartCoroutine(SetDetailsText("Purchased item!"));
                    }
                    break;
                case PurchaseResultType.FAILURE_NOT_ENOUGH_POINTS:
                    StartCoroutine(SetDetailsText("Not enough points!"));
                    break;
                case PurchaseResultType.FAILURE_OUT_OF_STOCK:
                    StartCoroutine(SetDetailsText("This item is out of stock!"));
                    break;
            }
        }
    }
}