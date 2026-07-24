using System.Collections.Generic;
using CourseNameSpace;
using RegistrationNameSpace;

namespace GameInfoSpace
{
    public interface IGameInfo
    {
        public List<Course> CourseList { get; set; }
        public int MaxCredits { get; set; }
    }
}