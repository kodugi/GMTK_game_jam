namespace CourseNameSpace
{
    public class Course
    {
        public int CourseID { get; private set; }
        public string CourseName { get; private set; }
        public int Credits { get; private set; }
        public CourseType CourseType { get; private set; }
        public int Quota { get; private set; }
        public int CurrentQuota { get; private set; }
        public float Rating { get; private set; }

        public Course(int courseID, string courseName, int credits, CourseType courseType, int quota, int currentQuota, float rating)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            CourseType = courseType;
            Quota = quota;
            CurrentQuota = currentQuota;
            Rating = rating;
        }
    }

    public enum CourseType
    {
        ESSENTIAL_GE = 0,
        NON_ESSENTIAL_GE = 1,
        ESSENTIAL_MAJOR = 2,
        NON_ESSENTIAL_MAJOR = 3
    }
}