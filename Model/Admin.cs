using System;
namespace StudentApp.Model
{
    public class Admin : User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Id { get; set; }
    }
}
