namespace ShopNameSpace
{
    public class WalletManager
    {
        public int Points { get; private set; }
        
        private ItemManager _itemManager;
        private DetailsView _detailsView;

        public void Initialize(int points, ItemManager itemManager, DetailsView detailsView)
        {
            Points = points;
            _itemManager = itemManager;
            _detailsView = detailsView;

            _itemManager.RaiseTryPurchaseEvent += HandleTryPurchaseEvent;
        }

        public bool HasEnoughPointsToPurchase(ItemEntry item)
        {
            return item.ItemData.Cost <= Points;
        }

        public void PurchaseItem(ItemEntry item)
        {
            if (!HasEnoughPointsToPurchase(item))
            {
                return;
            }
            
            Points -= item.ItemData.Cost;
            _detailsView.SetPointsText(Points);
        }

        private void HandleTryPurchaseEvent(object sender, TryPurchaseEventArgs e)
        {
            if (e.Result == PurchaseResultType.SUCCESS)
            {
                PurchaseItem(e.Item);
            }
        }
    }
}