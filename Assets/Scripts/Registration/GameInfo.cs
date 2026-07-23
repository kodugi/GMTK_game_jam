using System.Collections.Generic;
using CourseNameSpace;

namespace RegistrationNameSpace
{
    public class GameInfo
    {
        public static GameInfo Instance;
        
        public List<Course> CourseList { get; private set; }
        public double TimeOffset { get; private set; }
        public double TargetTime{ get; private set; }
        public int MaxCredits { get; private set; }
        
        public GameInfo(List<Course> courseList, double timeOffset, double targetTime, int maxCredits)
        {
            Instance = this;
            
            CourseList = courseList;
            TimeOffset = timeOffset;
            TargetTime = targetTime;
            MaxCredits = maxCredits;
        }
    }
}