namespace SchoolScore.Api.Models
{
    public class RegisteredOpenSubject : PagingModel<ClassroomOpenSubject>
    {
        public IEnumerable<DbModels.Subject> Subjects { get; set; }
        public IEnumerable<DbModels.OpenSubject> OpenSubjects { get; set; }
    }

    public class ClassroomOpenSubject
    {
        public DbModels.Classroom Classroom { get; set; }
        public IEnumerable<string> OpenSubjectIds { get; set; }
        public IEnumerable<DbModels.ClassroomStudent> ClassroomStudents { get; set; }
    }

    public class ClassroomOpenSubjectDetails : PagingModel<StudentInClassroom>
    {
        public DbModels.Classroom Classroom { get; set; }
        public DbModels.Teacher Teacher { get; set; }
        public DbModels.Subject Subject { get; set; }
        public DbModels.LearningArea LearningArea { get; set; }
        public DbModels.OpenSubject OpenSubject { get; set; }
    }

    public class RegisteredOpenSubjectTimeTable : ClassroomOpenSubjectDetails
    {
        public IEnumerable<TimeTable> TimeTables { get; set; }
    }

    public class TimeTable
    {
        public int Month { get; set; }
        public int Week { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public int DayNo { get; set; }
    }
}
