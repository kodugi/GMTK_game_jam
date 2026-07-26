using System;
using System.Collections.Generic;
using PersistentDataNameSpace;
using UnityEngine;
using UnityEngine.UI;

namespace ShopNameSpace
{
    public class PerksView: MonoBehaviour
    {
        [SerializeField] private GameObject _perksPanel;
        [SerializeField] private GameObject _perkPrefab;
        [SerializeField] private List<PerkSpritesDictionaryEntry> _perkSpritesDictionaryEntries;

        private List<GameObject> _spawnedPerks;
        private Dictionary<ItemCode, Sprite> _perkSpritesDict = new Dictionary<ItemCode, Sprite>();

        public void Initialize(ItemManager itemManager)
        {
            itemManager.RaiseTryPurchaseEvent += HandleTryPurchaseEvent;
            foreach (PerkSpritesDictionaryEntry entry in _perkSpritesDictionaryEntries)
            {
                _perkSpritesDict[entry.itemCode] = entry.Sprite;
            }
        }

        private void HandleTryPurchaseEvent(object sender, TryPurchaseEventArgs e)
        {
            if (e.Result == PurchaseResultType.SUCCESS)
            {
                UpdatePerksList();
            }
        }

        private void UpdatePerksList()
        {
            if (_spawnedPerks != null && _spawnedPerks.Count > 0)
            {
                foreach (GameObject spawnedPerk in _spawnedPerks)
                {
                    Destroy(spawnedPerk);
                }
            }

            foreach (ItemData perkData in PersistentData.PerkList)
            {
                GameObject perk = Instantiate(_perkPrefab, _perksPanel.transform);
                _perkSpritesDict.TryGetValue(perkData.ItemCode, out Sprite sp);
                perk.GetComponent<Image>().sprite = sp;
            }
        }
    }
    
    [Serializable]
    public class PerkSpritesDictionaryEntry
    {
        public ItemCode itemCode;
        public Sprite Sprite;
    }
}