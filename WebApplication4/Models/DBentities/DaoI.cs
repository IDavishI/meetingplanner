using Schedule.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanner.Models
{
    interface DaoI
    {
        long UserRegistration(User user);
        void OrganizationRegistration(Organization organization);
        User getUserByLoginAndPassword(string login, string password);
    }
}
