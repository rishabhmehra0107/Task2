using System;
using StudentApp.Model;
using StudentApp.Services;
using static StudentApp.Model.Constants;
using StudentApp.Services.Utilities;
using Newtonsoft.Json;
using System.IO;

namespace StudentApp
{
    public class StudentApplication
	{
        private SchoolService SchoolService { get; set; }
        public School School { get; set; }
        public Admin LoggedInAdmin;

        public StudentApplication()
        {
            this.School = new School();
            this.SchoolService = new SchoolService(this.School);
            this.MainMenu();
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to Student Management System\n\n1. Setup New School\n2. School Login\n3. Exit\n\nPlease provide valid input from menu options :");

            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                MenuOption menuOption = (MenuOption)option;
                switch (menuOption)
                {
                    case MenuOption.Setup:
                        this.SetupSchool();
                        break;
                    case MenuOption.Login:
                        this.SchoolLogin();
                        break;
                    case MenuOption.Exit:
                        this.Exit();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please select option from the list");
                        this.MainMenu();
                        break;
                }
                this.MainMenu();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Please enter valid number to choose your option");
                this.MainMenu();
            }
        }

        public void SetupSchool()
        {
            this.School.Name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter School Name").ToUpper();
            this.School.Location = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter School Location");
            this.School.PinCode = Utility.GetStringInput("^[0-9]+$", "Enter School Pin Code");
            Console.WriteLine("School setup is completed.\nSchool Name: {0} , Location: {1} , PinCode: {2}", School.Name, School.Location, School.PinCode);

            Console.WriteLine("Please provide admin details to setup");

            Admin admin = new Admin()
            {
                Name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Admin Name").ToUpper(),
                UserName = Utility.GetStringInput("^[a-zA-Z0-9@._]+$", "Enter Admin Username").ToLower(),
                Password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Admin Password"),
                Age = Utility.GetIntInput("Enter Admin Age"),
                BloodGroup = Utility.GetStringInput("^[a-zA-Z+-]+$", "Enter Admin Blood Group").ToUpper(),
                Sex = Utility.GetStringInput("^[a-zA-Z]+$", "Enter Admin Sex").ToUpper()
            };

            if (this.SchoolService.AddAdmin(admin))
                Console.WriteLine("Admin added successfully!");
            else
                Console.WriteLine("Error!");

            Console.WriteLine("School Name: {0}, Admin Name: {1}, Admin Username: {2}", this.School.Name, admin.Name, admin.UserName);
        }

        public void SchoolLogin()
        {
            string userName = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Admin Username");
            string password = Utility.GetStringInput("^[a-zA-Z0-9]+$", "Enter Admin Password");

            this.LoggedInAdmin = this.SchoolService.LogIn(userName, password);
            try
            {
                if (this.LoggedInAdmin == null)
                {
                    Console.WriteLine("Invalid Credentials");
                    this.SchoolLogin();
                }
                else
                {
                    string name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter School Name");
                    if (this.School.Name.ToUpper().Equals(name.ToUpper()))
                        this.DisplaySchoolMenu();
                    else
                    {
                        Console.WriteLine("School Not Found!");
                        this.SchoolLogin();
                    }
                }
            }
            catch (Exception)
            {
                this.SchoolLogin();
            }
        }

        public void DisplaySchoolMenu()
        {
            Console.WriteLine("Welcome to {0} information management\n--------------------------------------\n", this.School.Name);

            Console.WriteLine("1. Add student\n2. Add marks for student\n3. Show student progress card\n4. Logout");
            try
            {
                int option = Convert.ToInt32(Console.ReadLine());
                SchoolMenu schoolMenu = (SchoolMenu)option;
                switch (schoolMenu)
                {
                    case SchoolMenu.AddStudent:
                        this.AddStudent();
                        break;
                    case SchoolMenu.AddMarks:
                        this.AddMarks();
                        break;
                    case SchoolMenu.ProgressCard:
                        this.ShowProgressCard();
                        break;
                    case SchoolMenu.Logout:
                        this.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Please select option from the list");
                        break;

                }

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                this.DisplaySchoolMenu();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid number to choose your option");
                this.DisplaySchoolMenu();
            }
        }

        public void AddStudent()
        {
            Student student = new Student()
            {
                Name = Utility.GetStringInput("^[a-zA-Z ]+$", "Enter Student Name :").ToUpper(),
                RollNumber = Utility.GetIntInput("Enter Student Roll Number :"),
                Age = Utility.GetIntInput("Enter Student Age"),
                BloodGroup = Utility.GetStringInput("^[a-zA-Z+-]+$", "Enter Student Blood Group").ToUpper(),
                Sex = Utility.GetStringInput("^[a-zA-Z]+$", "Enter Student Sex").ToUpper()
            };

            if (this.SchoolService.AddStudent(student))
                Console.WriteLine("Student added successfully!");
            else
                Console.WriteLine("Error! Student not added");
        }

        public void AddMarks()
        {
            try
            {
                int rollNumber = Utility.GetIntInput("Enter Student Roll Number :");

                var student = this.School.Students.Find(student => student.RollNumber.Equals(rollNumber));
                if (student != null)
                {
                    SubjectScore subjectScore = new SubjectScore()
                    {
                        Telugu = Utility.GetIntInput("Enter Marks Scored in Telugu :"),
                        Hindi = Utility.GetIntInput("Enter Marks Scored in Hindi :"),
                        English = Utility.GetIntInput("Enter Marks Scored in English :"),
                        Maths = Utility.GetIntInput("Enter Marks Scored in Maths :"),
                        Science = Utility.GetIntInput("Enter Marks Scored in Science :"),
                        Social = Utility.GetIntInput("Enter Marks Scored in Social :")
                    };

                    double totalMarks = subjectScore.Telugu + subjectScore.Hindi + subjectScore.English + subjectScore.Maths + subjectScore.Science + subjectScore.Social;

                    if (this.SchoolService.AddScore(subjectScore, student.RollNumber, totalMarks))
                        Console.WriteLine("Student marks are added successfully");
                    else
                        Console.WriteLine("Error! Student marks could not be added");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }

        public void ShowProgressCard()
        {
            try
            {
                int rollNumber = Utility.GetIntInput("Enter Student Roll Number :");

                var student = this.School.Students.Find(student => student.RollNumber.Equals(rollNumber));
                if (student != null)
                {
                    Console.WriteLine("Student Roll Number : {0}\nStudent Name : {1}\nStudent Marks\n-------------", student.RollNumber, student.Name);
                    foreach(SubjectScore subject in student.SubjectsScore)
                    {
                        Console.WriteLine("Telugu : {0}\nHindi : {1}\nEnglish : {2}\nMaths : {3}\nScience : {4}\nSocial : {5}", subject.Telugu, subject.Hindi, subject.English, subject.Maths, subject.Science, subject.Social);
                    }

                    Console.WriteLine("-------------\n\nTotal Marks : {0}\nPercentage : {1}\n-------------", student.TotalMarks, student.Percentage);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Student Progress Card can't be displayed.");
            }
        }

        public void Exit()
        {
            Console.WriteLine("Goodbye ", this.LoggedInAdmin.Name);

            string schoolResultJson = JsonConvert.SerializeObject(this.School) + JsonConvert.SerializeObject(this.School.Admin) + JsonConvert.SerializeObject(this.School.Students);
            File.WriteAllText(@"student.json", schoolResultJson);

            this.LoggedInAdmin = new Admin();
        }
    }
}