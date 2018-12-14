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
        public List<Schedule> events { get; set; }
    }
}
