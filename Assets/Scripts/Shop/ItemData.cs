using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using UnityEngine;

namespace ShopNameSpace
{
    public class ItemData
    {
        public virtual ItemType ItemType { get; private set; }
        public virtual int Cost { get; private set; }
        public virtual string ItemName { get; private set; }
        public virtual string ItemDescription { get; private set; }

        public ItemData(ItemType itemType, int cost, string itemName, string itemDescription)
        {
            ItemType = itemType;
            Cost = cost;
            ItemName = itemName;
            ItemDescription = itemDescription;
        }

        protected ItemData()
        {
            
        }

        public virtual void OnPurchase(WalletManager walletManager)
        {
            
        }

        public virtual int GetExtraPoints(List<Course> registeredCourseList)
        {
            return 0;
        }

        public virtual void OnRelease()
        {
            
        }
    }

    public enum ItemType
    {
        None = 0,
        IncreaseMaxCredits = 1
    }

    public class IncreaseMaxCreditsItem: ItemData
    {
        public override ItemType ItemType => ItemType.IncreaseMaxCredits;
        public override int Cost => 5;

        public override string ItemName => "Petition";

        public override string ItemDescription => "Upon purchase, increases max credits by 3";
        private int _incrementAmount = 3;

        public override void OnPurchase(WalletManager walletManager)
        {
            PersistentData.SetMaxCredits(PersistentData.MaxCredits + _incrementAmount);
            Debug.Log("current max credits: " + PersistentData.MaxCredits);
        }
    }
}