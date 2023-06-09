﻿namespace SchoolScore.Api.Models
{
    public class ClassroomStudent : DbModels.ClassroomStudent
    {
        public DbModels.Classroom Classroom { get; set; }
        public DbModels.Student Student { get; set; }
    }

    public class StudentInClassroom : DbModels.ClassroomStudent
    {
        public DbModels.Student Student { get; set; }
    }
}
