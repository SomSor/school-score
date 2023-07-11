﻿namespace SchoolScore.Api.DbModels
{
    public class Account : DbModelBase
    {
        public string PersonId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ActivateDate { get; set; }
        public string SuspendDate { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public class RolePossible
        {
            public readonly string Admin = nameof(Admin);
            public readonly string Mod = nameof(Mod);
            public readonly string Teacher = nameof(Teacher);
            public readonly string Student = nameof(Student);
        }
    }
}
