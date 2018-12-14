using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class User
    {
        public long id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public List<UserGroup> groups { get; set; }
        
        public User() {}

        public User(long id, string login)
        {
            this.id = id;
            this.login = login;
        }

        public User(long id, string login, string password, string email, string role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.email = email;
            this.role = role;
        }
    }
}
