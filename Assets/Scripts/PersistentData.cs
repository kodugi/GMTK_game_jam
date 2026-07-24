using System.Collections.Generic;
using CourseNameSpace;
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
        
        public static void SetMaxCredits(int credits)
        {
            MaxCredits = credits;
        }
    }
}