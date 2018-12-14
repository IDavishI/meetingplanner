using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Instructor
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string specialization { get; set; }
        public long courseId { get; set; }
        public Course course { get; set; }

        public Instructor(long id, string firstName, string secondName, string specialization)
        {
            this.id = id;
            this.firstName = firstName;
            this.secondName = secondName;
            this.specialization = specialization;
        }
    }
}
