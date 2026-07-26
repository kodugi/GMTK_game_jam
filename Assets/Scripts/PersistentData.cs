using System.Collections.Generic;
using CourseNameSpace;
using NUnit.Framework;
using ShopNameSpace;

namespace PersistentDataNameSpace
{
    public class PersistentData
    {
        public static List<Course> TakenCourseList = new List<Course>();
        public static int MaxCredits = 20;
        public static int MaxCreditsLevel = 0;
        public static int CourseSlots = 7;
        public static int CourseSlotsLevel = 0;
        public static int ItemSlots = 5;
        public static int PerkSlots = 5;
        public static List<ItemData> PerkList = new List<ItemData>();
        public static int Points = 0;
        public static Dictionary<CourseType, int> CourseTypePool = new Dictionary<CourseType, int>();
        public static Dictionary<DepartmentType, int> DepartmentPool = new Dictionary<DepartmentType, int>();
        public static int Round = 0;
        public static bool IsInitialized = false;
        
        public static void SetMaxCredits(int credits)
        {
            MaxCredits = credits;
        }
        
        public static void InitializePersistentData()
        {
            if (IsInitialized)
            {
                return;
            }
            
            TakenCourseList = new List<Course>();
            MaxCredits = 20;
            MaxCreditsLevel = 0;
            CourseSlots = 7;
            CourseSlotsLevel = 0;
            PerkList = new List<ItemData>();
            Points = 0;
            CourseTypePool = new Dictionary<CourseType, int>();
            DepartmentPool = new Dictionary<DepartmentType, int>();
            FillPools();
            Round = 0;
            IsInitialized = true;
        }

        private static void FillPools()
        {
            CourseTypePool.Add(CourseType.ESSENTIAL_GE, 6);
            CourseTypePool.Add(CourseType.NON_ESSENTIAL_GE, 4);
            CourseTypePool.Add(CourseType.ESSENTIAL_MAJOR, 3);
            CourseTypePool.Add(CourseType.NON_ESSENTIAL_MAJOR, 2);
            
            for (int i = 1; i < System.Enum.GetValues(typeof(DepartmentType)).Length; i++)
            {
                DepartmentPool.Add((DepartmentType)i, 2);
            }
        }

        public static int GetTargetPoints()
        {
            // TODO: Elaborate on target calculation
            return 10 + Round * 10;
        }
    }
}