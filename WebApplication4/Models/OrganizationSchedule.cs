using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Models
{
    public class OrganizationSchedule
    {
        public long id { get; set; }
        public long organizationId { get; set; }
        public Organization organization { get; set; }
        public long scheduleId { get; set; }
        public Schedule schedule { get; set; }
    }
}
