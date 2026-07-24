using System.Collections.Generic;
using CourseNameSpace;
using GameInfoSpace;

namespace ReservationNameSpace
{
    public class ReservationInfo: IGameInfo
    {
        public List<Course> CourseList{get; set;}
        public int MaxCredits { get; set; }
        
        public ReservationInfo(List<Course> courseList, int maxCredits)
        {
            CourseList = courseList;
            MaxCredits = maxCredits;
        }
    }
}