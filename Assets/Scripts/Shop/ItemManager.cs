using System;
using System.Collections.Generic;
using System.Linq;
using PersistentDataNameSpace;

namespace ShopNameSpace
{
    public class ItemManager
    {
        private WalletManager _walletManager;
        
        public List<ItemEntry> ItemList { get; private set; }
        public List<ItemEntry> PerkList { get; private set; }
        private List<ItemData> _itemDataList;
        private List<ItemData> _perkDataList;
        
        public event EventHandler<TryPurchaseEventArgs> RaiseTryPurchaseEvent;

        public void Initialize(List<ItemData> itemDataList, List<ItemData> perkDataList, WalletManager walletManager)
        {
            _itemDataList = itemDataList;
            _perkDataList = perkDataList;
            
            ItemList = GenerateItemList();
            PerkList = GeneratePerkList();
            _walletManager = walletManager;
        }

        private List<ItemEntry> GenerateItemList()
        {
            List<ItemEntry> itemList = new List<ItemEntry>();

            IEnumerable<ItemData> selected = _itemDataList.OrderBy(x => Guid.NewGuid()).Take(5);
            // TODO: elaborate on the item selection logic
            foreach (ItemData itemData in selected)
            {
                itemList.Add(new ItemEntry(itemData));
            }
            return itemList;
        }
        
        private List<ItemEntry> GeneratePerkList()
        {
            List<ItemEntry> perkList = new List<ItemEntry>();

            IEnumerable<ItemData> selected = _perkDataList.OrderBy(x => Guid.NewGuid()).Take(5);

            foreach (ItemData perkData in selected)
            {
                perkList.Add(new ItemEntry(perkData));
            }
            return perkList;
        }

        public void TryPurchaseItem(int idx)
        {
            ItemEntry item = ItemList[idx];
            PurchaseResultType result = GetPurchaseResult(item);
            if (result == PurchaseResultType.SUCCESS)
            {
                item.Purchase(_walletManager);
                RaiseTryPurchaseEvent?.Invoke(this, new TryPurchaseEventArgs(PurchaseResultType.SUCCESS, item));
            }
            else
            {
                RaiseTryPurchaseEvent?.Invoke(this, new TryPurchaseEventArgs(result, null));
            }
        }
        
        public void TryPurchasePerk(int idx)
        {
            ItemEntry perk = PerkList[idx];
            PurchaseResultType result = GetPurchaseResult(perk);
            if (result == PurchaseResultType.SUCCESS)
            {
                perk.Purchase(_walletManager);
                PersistentData.PerkList.Add(perk.ItemData);
                RaiseTryPurchaseEvent?.Invoke(this, new TryPurchaseEventArgs(PurchaseResultType.SUCCESS, perk));
            }
            else
            {
                RaiseTryPurchaseEvent?.Invoke(this, new TryPurchaseEventArgs(result, null));
            }
        }

        private PurchaseResultType GetPurchaseResult(ItemEntry item)
        {
            if (item.IsSoldOut)
            {
                return PurchaseResultType.FAILURE_OUT_OF_STOCK;
            }

            if (!_walletManager.HasEnoughPointsToPurchase(item))
            {
                return PurchaseResultType.FAILURE_NOT_ENOUGH_POINTS;
            }
            return PurchaseResultType.SUCCESS;
        }
    }

    public class ItemEntry
    {
        public ItemData ItemData { get; private set; }
        public bool IsSoldOut { get; private set; }

        public ItemEntry(ItemData itemData)
        {
            ItemData = itemData;
            IsSoldOut = false;
        }

        public void Purchase(WalletManager walletManager)
        {
            ItemData.OnPurchase(walletManager);
            IsSoldOut = true;
        }
    }

    public enum PurchaseResultType
    {
        SUCCESS = 0,
        FAILURE_NOT_ENOUGH_POINTS = 1,
        FAILURE_OUT_OF_STOCK = 2
    }

    public class TryPurchaseEventArgs : EventArgs
    {
        public PurchaseResultType Result { get; private set; }
        public ItemEntry Item { get; private set; }
        
        public TryPurchaseEventArgs(PurchaseResultType result, ItemEntry item)
        {
            Result = result;
            Item = item;
        }
    }
}