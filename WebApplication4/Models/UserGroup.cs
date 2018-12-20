using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class UserGroup
    {
        public UserGroup(Group group)
        {
            this.group = group;
        }

        public UserGroup(User user, Group group)
        {
            this.user = user;
            this.group = group;
        }
        
        [JsonIgnore]
        public int id { get; set; }
        [JsonIgnore]
        public int userId { get; set; }
        [JsonIgnore]
        public User user { get; set; }
        [JsonIgnore]
        public int groupId { get; set; }
        public Group group { get; set; }
    }
}
