using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class Room
    {
        public long id { get; set; }
        public string number { get; set; }
        public int floor { get; set; }
        public List<Schedule> events { get; set; }
    }
}
