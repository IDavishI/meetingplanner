using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Organization
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string specialization { get; set; }
        public long personId { get; set; }
        public User head { get; set; }
        public List<OrganizationSchedule> events { get; set; }
        public List<Course> courses { get; set; }
        public Organization()
        {
     }

        public Organization(long id, string name, string description, string specialization)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.specialization = specialization;
        }
    }
}
