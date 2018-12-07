using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Group
    {
        public long id { get; set; }
        public string name { get; set; }
        public List<Schedule> events { get; set; }
        public List<UserGroup> users { get; set; }
    }
}
