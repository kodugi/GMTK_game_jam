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

    public class IncreaseMaxCreditsItem: ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        
        public override int Cost => 10 + 5 * PersistentData.MaxCreditsLevel;

        public override string ItemName => "Please Increase Maximum Credit Limitations!";

        public override string ItemDescription => "Increases max credits by " + _incrementAmount;
        private readonly int _incrementAmount = 3;

        public override void OnPurchase(WalletManager walletManager)
        {
            PersistentData.SetMaxCredits(PersistentData.MaxCredits + _incrementAmount);
            PersistentData.MaxCreditsLevel++;
        }
    }
    
    public class IncreaseCourseSlotsItem: ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        
        public override int Cost => 10 + 5 * PersistentData.CourseSlotsLevel;

        public override string ItemName => "Please Increase the Number of Courses!";

        public override string ItemDescription => "Increases the number of course slots by " + _incrementAmount;
        private readonly int _incrementAmount = 1;

        public override void OnPurchase(WalletManager walletManager)
        {
            PersistentData.CourseSlots += _incrementAmount;
            PersistentData.CourseSlotsLevel++;
        }
    }

    public class IncreaseEssentialGEItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;

        public override int Cost => 10;

        public override string ItemName => "Please Open More Essential GE Courses!";

        public override string ItemDescription => "Adds one essential GE course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.CourseTypePool.GetValueOrDefault(CourseType.ESSENTIAL_GE, 0);
            PersistentData.CourseTypePool[CourseType.ESSENTIAL_GE] = currentAmount + 1;
        }
    }
    
    public class IncreaseElectiveGEItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;

        public override int Cost => 10;

        public override string ItemName => "Please Open More Elective GE Courses!";

        public override string ItemDescription => "Adds one elective GE course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.CourseTypePool.GetValueOrDefault(CourseType.NON_ESSENTIAL_GE, 0);
            PersistentData.CourseTypePool[CourseType.NON_ESSENTIAL_GE] = currentAmount + 1;
        }
    }
    
    public class IncreaseEssentialMajorItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;

        public override int Cost => 10;

        public override string ItemName => "Please Open More Essential Major Courses!";

        public override string ItemDescription => "Adds one essential major course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.CourseTypePool.GetValueOrDefault(CourseType.ESSENTIAL_MAJOR, 0);
            PersistentData.CourseTypePool[CourseType.ESSENTIAL_MAJOR] = currentAmount + 1;
        }
    }
    
    public class IncreaseElectiveMajorItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;

        public override int Cost => 10;

        public override string ItemName => "Please Open More Elective Major Courses!";

        public override string ItemDescription => "Adds one elective major course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.CourseTypePool.GetValueOrDefault(CourseType.NON_ESSENTIAL_MAJOR, 0);
            PersistentData.CourseTypePool[CourseType.NON_ESSENTIAL_MAJOR] = currentAmount + 1;
        }
    }

    public class IncreaseHumanLiteratureItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        public override int Cost => 10;
        public override string ItemName => "Please Open More Human Literature Courses!";

        public override string ItemDescription => "Adds one human literature course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.DepartmentPool.GetValueOrDefault(DepartmentType.HUMAN_LITERATURE, 0);
            PersistentData.DepartmentPool[DepartmentType.HUMAN_LITERATURE] = currentAmount + 1;
        }
    }
    
    public class IncreaseNaturalSciencesItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        public override int Cost => 10;
        public override string ItemName => "Please Open More Natural Sciences Courses!";

        public override string ItemDescription => "Adds one natural sciences course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.DepartmentPool.GetValueOrDefault(DepartmentType.NATURAL_SCIENCES, 0);
            PersistentData.DepartmentPool[DepartmentType.NATURAL_SCIENCES] = currentAmount + 1;
        }
    }
    
    public class IncreaseSocialSciencesItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        public override int Cost => 10;
        public override string ItemName => "Please Open More Social Sciences Courses!";

        public override string ItemDescription => "Adds one social sciences course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.DepartmentPool.GetValueOrDefault(DepartmentType.SOCIAL_SCIENCES, 0);
            PersistentData.DepartmentPool[DepartmentType.SOCIAL_SCIENCES] = currentAmount + 1;
        }
    }
    
    public class IncreaseEngineeringItem : ItemData
    {
        public override ItemType ItemType => ItemType.Item;
        public override int Cost => 10;
        public override string ItemName => "Please Open More Engineering Courses!";

        public override string ItemDescription => "Adds one engineering course into the course pool";

        public override void OnPurchase(WalletManager walletManager)
        {
            int currentAmount = PersistentData.DepartmentPool.GetValueOrDefault(DepartmentType.ENGINEERING, 0);
            PersistentData.DepartmentPool[DepartmentType.ENGINEERING] = currentAmount + 1;
        }
    }

    public class Notebook : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        
        public override int Cost => 10;
        
        public override string ItemName => "Notebook";

        public override string ItemDescription => "When in inventory, gives a fixed point gain of " + _incrementAmount + " points.";

        private readonly int _incrementAmount = 5;

        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            record.ExtraTotal += _incrementAmount;
            return record;
        }
    }

    public class ExtraPointsForSpecificDepartmentsPerk : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        public override int Cost => 10;
        protected virtual int IncrementAmount => 10;
        protected virtual List<DepartmentType> DepartmentTypes => new List<DepartmentType>();
        protected virtual string DepartmentNames => "";

        public override string ItemDescription => "When in inventory, gives a fixed point gain of " + IncrementAmount + " points for courses from the " + DepartmentNames + ".";
    }

    public class VoiceRecorder : ExtraPointsForSpecificDepartmentsPerk
    {
        public override string ItemName => "Voice recorder";

        protected override List<DepartmentType> DepartmentTypes => new List<DepartmentType>{DepartmentType.HUMAN_LITERATURE, DepartmentType.SOCIAL_SCIENCES};
        protected override string DepartmentNames => "College of Human Literature and Social Sciences";
    }
    
    public class Calculator : ExtraPointsForSpecificDepartmentsPerk
    {
        public override string ItemName => "Calculator";
        protected override List<DepartmentType> DepartmentTypes => new List<DepartmentType>{DepartmentType.NATURAL_SCIENCES, DepartmentType.ENGINEERING};
        protected override string DepartmentNames => "College of Natural Sciences and Engineering";
    }

    public class AlarmClock : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        public override int Cost => 10;

        public override string ItemName => "Alarm clock";
        private readonly float _multiplier = 1.5f;
        public override string ItemDescription => "When in inventory, gains X" + _multiplier.ToString("F2") + " points from courses that start earlier than 11 AM.";
        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            foreach (CoursePointsEntry coursePointsEntry in record.CoursePointsEntries)
            {
                bool isEarly = false;
                foreach (TimetableEntry timetableEntry in coursePointsEntry.Course.Timetable)
                {
                    if (timetableEntry.StartHour * 60 + timetableEntry.StartMinute < 660)
                    {
                        isEarly = true;
                    }
                }

                if (isEarly)
                {
                    coursePointsEntry.Multiplier *= _multiplier;
                }
            }

            return record;
        }
    }
    
    public class Coffee : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        public override int Cost => 10;

        public override string ItemName => "Coffee";
        private readonly float _multiplier = 2f;
        public override string ItemDescription => "When in inventory, gains X" + _multiplier.ToString("F2") + " points from courses that end later than 6 PM.";
        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            foreach (CoursePointsEntry coursePointsEntry in record.CoursePointsEntries)
            {
                bool isLate = false;
                foreach (TimetableEntry timetableEntry in coursePointsEntry.Course.Timetable)
                {
                    if (timetableEntry.EndHour * 60 + timetableEntry.EndMinute > 18 * 60)
                    {
                        isLate = true;
                    }
                }

                if (isLate)
                {
                    coursePointsEntry.Multiplier *= _multiplier;
                }
            }

            return record;
        }
    }

    public abstract class GetExtraPointsIfContainsMultipleCoursesFromDepartment: ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        public override int Cost => 20;

        protected virtual int ExtraPoints => 30;
        protected virtual int Count => 3;
        protected abstract bool ShouldCount(CoursePointsEntry coursePointsEntry);
        protected virtual string Condition => "";
        
        public override string ItemDescription => "When in inventory, gains " + ExtraPoints + " extra points if registered courses contain " + Count + " or more courses " + Condition + ".";
        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            int cnt = 0;
            
            foreach (CoursePointsEntry coursePointsEntry in record.CoursePointsEntries)
            {
                if (ShouldCount(coursePointsEntry))
                {
                    cnt++;
                }
            }

            if (cnt >= Count)
            {
                record.ExtraTotal += ExtraPoints;
            }

            return record;
        }
    }

    public class Thesaurus: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override string ItemName => "Thesaurus";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Department == DepartmentType.HUMAN_LITERATURE;
        }

        protected override string Condition => "that are from the College of Human Literature";
    }
    
    public class LabCoat: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override string ItemName => "Lab coat";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Department == DepartmentType.NATURAL_SCIENCES;
        }

        protected override string Condition => "that are from the College of Natural Sciences";
    }
    
    public class Newspaper: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override string ItemName => "Newspaper";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Department == DepartmentType.SOCIAL_SCIENCES;
        }

        protected override string Condition => "that are from the College of Social Sciences";
    }
    
    public class MacBook: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override string ItemName => "MacBook";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Department == DepartmentType.ENGINEERING;
        }

        protected override string Condition => "that are from the College of Engineering";
    }
    
    public class Encyclopedia: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override int Cost => 10;
        protected override int Count => 5;
        public override string ItemName => "Encyclopedia";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.CourseType == CourseType.ESSENTIAL_GE || coursePointsEntry.Course.CourseType == CourseType.NON_ESSENTIAL_GE;
        }

        protected override string Condition => "that belong to GE courses";
    }
    
    public class Bicycle: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override int Cost => 10;

        public override string ItemName => "Bicycle";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Credits == 1;
        }

        protected override string Condition => "whose credits are 1";
    }
    
    public class CreditCard : ItemData
    {
        public override ItemType ItemType => ItemType.Perk;
        public override int Cost => -50;

        public override string ItemName => "Credit Card";
        private readonly int _deductionAmount = 5;
        public override string ItemDescription => "Upon purchase, gain 50 points. When in inventory, gives a fixed point loss of " + _deductionAmount + " points.";
        public override CoursePointsRecord GetExtraPoints(CoursePointsRecord record)
        {
            record.ExtraTotal -= _deductionAmount;
            return record;
        }
    }
    
    public class ArtOfWar: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override int Cost => 15;
        protected override int Count => 5;
        public override string ItemName => "Art of War";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.CurrentQuota >= 100;
        }

        protected override string Condition => "which was reserved by as much as, or more than, 100 people";
    }
    
    public class MichelinGuide: GetExtraPointsIfContainsMultipleCoursesFromDepartment
    {
        public override int Cost => 15;
        protected override int Count => 3;
        public override string ItemName => "Michelin Guide";

        protected override bool ShouldCount(CoursePointsEntry coursePointsEntry)
        {
            return coursePointsEntry.Course.Rating > 4.5f;
        }

        protected override string Condition => "whose average ratings are greater than 4.5";
    }
}