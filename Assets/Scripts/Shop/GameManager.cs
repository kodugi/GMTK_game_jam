using System.Collections.Generic;
using PersistentDataNameSpace;
using UnityEngine;

namespace ShopNameSpace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        [SerializeField] private DetailsView _detailsView;
        [SerializeField] private PopupView _popupView;
        [SerializeField] private PerksView _perksView;

        private ItemManager _itemManager;
        private WalletManager _walletManager;

        void Start()
        {
            _itemManager = new ItemManager();
            _walletManager = new WalletManager();
            
            _itemManager.Initialize(GenerateItemList(), GeneratePerkList(), _walletManager);
            _walletManager.Initialize(PersistentData.Points, _itemManager, _detailsView);
            _itemView.Initialize(_itemManager);
            _detailsView.Initialize(_walletManager);
            _popupView.Initialize(_itemManager);
            _perksView.Initialize(_itemManager);
        }

        private List<ItemData> GenerateItemList()
        {
            List<ItemData> itemList = new List<ItemData>();
            itemList.Add(new IncreaseMaxCreditsItem());
            return itemList;
        }

        private List<ItemData> GeneratePerkList()
        {
            List<ItemData> itemList = new List<ItemData>();
            itemList.Add(new ItemData(ItemType.None, 10, "testPerk", "this is a test perk"));
            return itemList;
        }
    }
}