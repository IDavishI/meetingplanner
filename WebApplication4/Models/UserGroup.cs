using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class UserGroup
    {
        public int id { get; set; }
        public int userId { get; set; }
        public User user { get; set; }
        public int groupId { get; set; }
        public Group group { get; set; }
    }
}
