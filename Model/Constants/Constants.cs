using System;
namespace StudentApp.Model
{
    public static class Constants
    {
        public static double TotalMarks = 0;

        public static double Percentage = 0;

        public enum MenuOption
        {
            Setup = 1,
            Login = 2,
            Exit = 3
        }

        public enum SchoolMenu
        {
            AddStudent = 1,
            AddMarks = 2,
            ProgressCard = 3,
            Logout = 4
        }
    }
}
