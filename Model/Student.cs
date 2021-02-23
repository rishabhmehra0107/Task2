using System;
using System.Collections.Generic;

namespace StudentApp.Model
{
    public class Student : User
    {
        public Student()
        {
            this.SubjectsScores = new List<Marks>();
        }

        public int RollNumber { get; set; }

        public double TotalMarks { get; set; }

        public List<Marks> SubjectsScores { get; set; }
    }
}
