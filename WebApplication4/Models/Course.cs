using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Course
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public long organizationId { get; set; }
        public List<Schedule> events { get; set; }
        public List<Group> groups { get; set; }

        public Course()
        {

        }

        public Course(long id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Course(string name, string description, long organizationId)
        {
            this.name = name;
            this.description = description;
            this.organizationId = organizationId;
        }
    }
}
