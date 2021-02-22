using System;
using System.Linq;
using System.Reflection;
using StudentApp.Model;
using static StudentApp.Model.Constants;

namespace StudentApp.Services
{
	public class SchoolService
	{
		public School School { get; set; }

		public SchoolService(School school)
		{
			this.School = school;
		}

		public Admin LogIn(string userName, string password)
		{
            try
            {
				var admin = new Admin();
				if (this.School.Admin.Any(admin => admin.UserName.ToLower() == userName.ToLower() && admin.Password == password))
				{
					admin = this.School.Admin.Find(admin => admin.UserName.ToLower() == userName.ToLower() && admin.Password == password);
				}

				return admin;
			}
            catch (Exception)
            {
				return null;
            }
		}

		public bool AddAdmin(Admin admin)
		{
			try
			{
				admin.Id = "Admin_"+this.School.Admin.Count + 1;
				this.School.Admin.Add(admin);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public bool AddStudent(Student student)
        {
            try
            {
				student.TotalMarks = DefaultTotalMarks;
				student.Percentage = DefaultPercentage;
				this.School.Students.Add(student);

				return true;
            }
            catch (Exception)
            {
				return false;
            }
        }

		public bool AddScore(SubjectScore subjectScore, int rollNumber, double totalMarks)
        {
            try
            {
				var student = this.School.Students.Find(student => student.RollNumber.Equals(rollNumber));
				if (student == null)
					return false;

				student.TotalMarks = totalMarks;
				student.Percentage = totalMarks / subjectScore.GetType().GetProperties().Count();
				student.SubjectsScores.Add(subjectScore);

				return true;
            }
            catch (Exception)
            {
				return false;
            }
        }
	}
}