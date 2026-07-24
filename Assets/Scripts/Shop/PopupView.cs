using RegistrationNameSpace;

namespace ShopNameSpace
{
    public class PopupView: PopupViewBase
    {
        public void Initialize(ItemManager itemManager)
        {
            base.Initialize();
            itemManager.RaiseTryPurchaseEvent += HandleTryPurchaseEvent;
        }

        private void HandleTryPurchaseEvent(object sender, TryPurchaseEventArgs e)
        {
            if (e.Result == PurchaseResultType.FAILURE_NOT_ENOUGH_POINTS)
            {
                ShowMessage("not enough points");
                return;
            }

            if (e.Result == PurchaseResultType.FAILURE_OUT_OF_STOCK)
            {
                ShowMessage("selected item is out of stock");
            }
        }
    }
}