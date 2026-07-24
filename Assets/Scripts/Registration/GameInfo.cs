using System.Collections.Generic;
using CourseNameSpace;
using GameInfoSpace;

namespace RegistrationNameSpace
{
    public class GameInfo: IGameInfo
    {
        public List<Course> CourseList { get; set; }
        public double TimeOffset { get; private set; }
        public double TargetTime{ get; private set; }
        public int MaxCredits { get; set; }
        
        public GameInfo(List<Course> courseList, double timeOffset, double targetTime, int maxCredits)
        {
            CourseList = courseList;
            TimeOffset = timeOffset;
            TargetTime = targetTime;
            MaxCredits = maxCredits;
        }
    }
}