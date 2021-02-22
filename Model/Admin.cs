using System;
namespace StudentApp.Model
{
    public class Admin : User
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
