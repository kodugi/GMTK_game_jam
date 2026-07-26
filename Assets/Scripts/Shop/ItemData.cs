using System.Collections.Generic;
using CourseNameSpace;
using PersistentDataNameSpace;
using UnityEngine;

namespace ShopNameSpace
{
    public class ItemData
    {
        public virtual ItemType ItemType { get; private set; }
        public virtual ItemCode ItemCode { get; private set; }
        public virtual int Cost { get; private set; }
        public virtual string ItemName { get; private set; }
        public virtual string ItemDescription { get; private set; }

        public ItemData(ItemType itemType, ItemCode itemCode, int cost, string itemName, string itemDescription)
        {
            ItemType = itemType;
            ItemCode = itemCode;
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

        public virtual CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            return record;
        }

        public virtual void OnRelease()
        {
            
        }
    }

    public class CoursePointsRecord
    {
        public List<CoursePointsEntry> CoursePointsEntries;
        public float ExtraTotal;
        
        public CoursePointsRecord()
        {
            CoursePointsEntries = new List<CoursePointsEntry>();
            ExtraTotal = 0;
        }
    }

    public class CoursePointsEntry
    {
        public Course Course;
        public float Multiplier;
        public float Extra;
        
        public CoursePointsEntry(Course course)
        {
            Course = course;
            Multiplier = 1;
            Extra = 0;
        }
    }

    public enum ItemType
    {
        Item = 0,
        Perk = 1
    }

    public enum ItemCode
    {
        None = 0,
        IncreaseMaxCredits = 1,
        IncreasePointGain = 2
    }

    public class IncreaseMaxCreditsItem: ItemData
    {
        public override ItemType ItemType => ItemType.Item;

        public override ItemCode ItemCode => ItemCode.IncreaseMaxCredits;
        public override int Cost => 5;

        public override string ItemName => "Petition";

        public override string ItemDescription => "Upon purchase, increases max credits by " + _incrementAmount;
        private readonly int _incrementAmount = 3;

        public override void OnPurchase(WalletManager walletManager)
        {
            PersistentData.SetMaxCredits(PersistentData.MaxCredits + _incrementAmount);
            Debug.Log("current max credits: " + PersistentData.MaxCredits);
        }
    }

    public class IncreasePointGainPerk : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        
        public override ItemCode ItemCode => ItemCode.IncreasePointGain;
        
        public override int Cost => 5;
        
        public override string ItemName => "Notebook";

        public override string ItemDescription => "When in the inventory, gives a fixed point gain of " + _incrementAmount + " points.";

        private readonly int _incrementAmount = 5;

        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            record.ExtraTotal += _incrementAmount;
            return record;
        }
    }
}