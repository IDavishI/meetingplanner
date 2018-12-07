using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Course
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Schedule> events { get; set; }
    }
}
