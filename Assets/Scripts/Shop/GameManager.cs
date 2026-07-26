using System.Collections.Generic;
using PersistentDataNameSpace;
using UnityEngine;

namespace ShopNameSpace
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private ItemView _itemView;
        [SerializeField] private DetailsView _detailsView;
        [SerializeField] private SidebarViewBase _sidebarViewBase;

        private ItemManager _itemManager;
        private WalletManager _walletManager;

        void Start()
        {
            PersistentData.InitializePersistentData();
            
            _itemManager = new ItemManager();
            _walletManager = new WalletManager();
            
            _itemManager.Initialize(GenerateItemList(), GenerateFixedItemList(), GeneratePerkList(), _walletManager);
            _walletManager.Initialize(PersistentData.Points, _itemManager, _detailsView);
            _itemView.Initialize(_itemManager);
            _detailsView.Initialize(_itemManager, _walletManager);
            _sidebarViewBase.Initialize(_itemManager);
        }

        private List<ItemData> GenerateItemList()
        {
            List<ItemData> itemList = new List<ItemData>();
            itemList.Add(new IncreaseEssentialGEItem());
            itemList.Add(new IncreaseElectiveGEItem());
            itemList.Add(new IncreaseEssentialMajorItem());
            itemList.Add(new IncreaseElectiveMajorItem());
            itemList.Add(new IncreaseHumanLiteratureItem());
            itemList.Add(new IncreaseNaturalSciencesItem());
            itemList.Add(new IncreaseSocialSciencesItem());
            itemList.Add(new IncreaseEngineeringItem());
            return itemList;
        }

        private List<ItemData> GenerateFixedItemList()
        {
            List<ItemData> itemList = new List<ItemData>();
            itemList.Add(new IncreaseMaxCreditsItem());
            itemList.Add(new IncreaseCourseSlotsItem());
            return itemList;
        }

        private List<ItemData> GeneratePerkList()
        {
            List<ItemData> itemList = new List<ItemData>();
            itemList.Add(new Notebook());
            // TODO: Add various kinds of perks
            itemList.Add(new VoiceRecorder());
            itemList.Add(new Calculator());
            itemList.Add(new AlarmClock());
            itemList.Add(new Coffee());
            itemList.Add(new Thesaurus());
            itemList.Add(new LabCoat());
            itemList.Add(new Newspaper());
            itemList.Add(new MacBook());
            itemList.Add(new Encyclopedia());
            itemList.Add(new Bicycle());
            itemList.Add(new CreditCard());
            itemList.Add(new ArtOfWar());
            itemList.Add(new MichelinGuide());
            return itemList;
        }
    }
}