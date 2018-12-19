using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Room
    {
        public long id { get; set; }
        public string number { get; set; }
        public int floor { get; set; }
        public long courseId { get; set; }
        public Course course { get; set; }

        public Room(long id, string number, int floor)
        {
            this.id = id;
            this.number = number;
            this.floor = floor;
        }

        public Room()
        {
        }
    }
}
