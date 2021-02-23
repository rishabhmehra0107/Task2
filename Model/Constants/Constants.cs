using System;
namespace StudentApp.Model
{
    public static class Constants
    {
        public enum MainMenuOption
        {
            Setup = 1,
            Login = 2,
            Exit = 3
        }

        public enum AdminMenuOption
        {
            AddStudent = 1,
            AddMarks = 2,
            ProgressCard = 3,
            Logout = 4
        }
    }
}
