using System;
namespace test.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
        public int UserType { get; set; }
        public string UserLogin_DateTime { get; set; }
    }
}