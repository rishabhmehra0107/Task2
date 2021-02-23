using System;
using System.Linq;
using StudentApp.Model;

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
				if (this.School.Admins.Any(admin => admin.UserName.ToLower() == userName.ToLower() && admin.Password == password))
				{
					admin = this.School.Admins.Find(admin => admin.UserName.ToLower() == userName.ToLower() && admin.Password == password);
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
				admin.Id = "Admin_"+this.School.Admins.Count + 1;
				this.School.Admins.Add(admin);

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
				this.School.Students.Add(student);

				return true;
            }
            catch (Exception)
            {
				return false;
            }
        }

		public bool AddScore(Marks subjectScore, int rollNumber, double totalMarks)
        {
            try
            {
				var student = this.School.Students.Find(student => student.RollNumber.Equals(rollNumber));
				if (student == null)
					return false;

				student.TotalMarks = totalMarks;
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