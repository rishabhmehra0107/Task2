using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StudentApp.Model
{
    public class School
    {
        public School()
        {
            this.Admins = new List<Admin>();
            this.Students = new List<Student>();
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public string PinCode { get; set; }

        public List<Admin> Admins { get; set; }

        public List<Student> Students { get; set; }

        public void UpdateSchoolJson(School school)
        {
            string schoolResultJson = JsonConvert.SerializeObject(school);
            File.WriteAllText(@"student.json", schoolResultJson);
        }
    }
}
