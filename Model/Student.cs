using System;
using System.Collections.Generic;

namespace StudentApp.Model
{
    public class Student : User
    {
        public Student()
        {
            this.SubjectsScores = new List<SubjectScore>();
        }

        public int RollNumber { get; set; }

        public double TotalMarks { get; set; }

        public double Percentage { get; set; }

        public List<SubjectScore> SubjectsScores { get; set; }
    }
}
