using System.Collections.Generic;
using CourseNameSpace;
using RegistrationNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace ShopNameSpace
{
    public class ItemView: MonoBehaviour
    {
        [SerializeField] private GameObject _itemPanel;
        [SerializeField] private GameObject _perkPanel;
        [SerializeField] private GameObject _itemEntry;
        [SerializeField] private GameObject _perkEntry;
        
        private ItemManager _itemManager;

        private List<GameObject> _itemEntries;
        private List<GameObject> _perkEntries;

        public void Initialize(ItemManager itemManager)
        {
            _itemManager = itemManager;
            SpawnItemEntries(_itemManager.ItemList);
            SpawnPerkEntries(_itemManager.PerkList);
        }

        private void SpawnItemEntries(List<ItemEntry> itemList)
        {
            if (_itemEntries != null && _itemEntries.Count > 0)
            {
                foreach (GameObject itemEntry in _itemEntries)
                {
                    GameObject.Destroy(itemEntry);
                }
            }
            
            _itemEntries = new List<GameObject>();
            for (int i = 0; i < itemList.Count; i++)
            {
                int idx = i;
                ItemEntry item = itemList[i];

                GameObject itemEntry = Instantiate(_itemEntry, _itemPanel.transform);
                itemEntry.GetComponent<ItemEntryView>().Initialize(item);
                itemEntry.GetComponentInChildren<Button>().onClick.AddListener(() => HandleItemPurchaseButtonClick(idx));
                _itemEntries.Add(itemEntry);
            }
        }
        
        private void SpawnPerkEntries(List<ItemEntry> perkList)
        {
            if (_perkEntries != null && _perkEntries.Count > 0)
            {
                foreach (GameObject perkEntry in _perkEntries)
                {
                    GameObject.Destroy(perkEntry);
                }
            }
            
            _perkEntries = new List<GameObject>();
            for (int i = 0; i < perkList.Count; i++)
            {
                int idx = i;
                ItemEntry perk = perkList[i];

                GameObject perkEntry = Instantiate(_perkEntry, _perkPanel.transform);
                perkEntry.GetComponent<PerkEntryView>().Initialize(perk);
                perkEntry.GetComponentInChildren<Button>().onClick.AddListener(() => HandlePerkPurchaseButtonClick(idx));
                _perkEntries.Add(perkEntry);
            }
        }

        private void HandleItemPurchaseButtonClick(int idx)
        {
            _itemManager.TryPurchaseItem(idx);
        }

        private void HandlePerkPurchaseButtonClick(int idx)
        {
            _itemManager.TryPurchasePerk(idx);
        }
    }
}