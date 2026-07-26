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
        public static int CourseSlots = 5;
        public static List<ItemData> PerkList = new List<ItemData>();
        public static int Points = 10;
        public static Dictionary<CourseType, int> CourseTypePool = new Dictionary<CourseType, int>();
        public static Dictionary<DepartmentType, int> DepartmentPool = new Dictionary<DepartmentType, int>();
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
            CourseSlots = 5;
            PerkList = new List<ItemData>();
            Points = 0;
            CourseTypePool = new Dictionary<CourseType, int>();
            DepartmentPool = new Dictionary<DepartmentType, int>();
            FillPools();
            IsInitialized = true;
        }

        private static void FillPools()
        {
            for (int i = 0; i < System.Enum.GetValues(typeof(CourseType)).Length; i++)
            {
                CourseTypePool.Add((CourseType)i, 2);
            }
            
            for (int i = 0; i < System.Enum.GetValues(typeof(DepartmentType)).Length; i++)
            {
                DepartmentPool.Add((DepartmentType)i, 1);
            }
        }
    }
}