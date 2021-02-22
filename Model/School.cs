using System;
using System.Collections.Generic;

namespace StudentApp.Model
{
    public class School
    {
        public School()
        {
            this.Admin = new List<Admin>();
            this.Students = new List<Student>();
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public string PinCode { get; set; }

        public List<Admin> Admin { get; set; }

        public List<Student> Students { get; set; }
    }
}
