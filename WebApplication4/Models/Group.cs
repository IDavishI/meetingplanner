using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Group
    {
        public long id { get; set; }
        public string name { get; set; }
        public long courseId { get; set; }
        public Course course { get; set; }
        public List<UserGroup> users { get; set; }

        public Group(long id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
