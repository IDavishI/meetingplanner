using MeetingPlanner.Models.DBentities;
using Schedule.Models;
using System;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace MeetingPlanner.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        DataBase dataBase = new DataBase();

        [HttpPost]
        [Route("/reg")]
        public string registration([FromBody] User user)
        {
            try
            {
                dataBase.UserRegistration(user);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/auth")]
        public string authorization([Microsoft.AspNetCore.Mvc.FromBody] User user)
        {
            try
            {
                user = dataBase.getUserByLoginAndPassword(user.login, user.password);
                if (user.role.Contains("ROLE_ADMIN"))
                {
                    Organization organization = dataBase.getOrganizationByHead(user.id);
                    organization.head = user;
                    return JsonConvert.SerializeObject(organization);
                }
                return JsonConvert.SerializeObject(user);
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/organizationreg")]
        public string organizationreg([Microsoft.AspNetCore.Mvc.FromBody] User user)
        {
            try
            {
                long userId = dataBase.UserRegistration(user);
                Organization organization = new Organization();
                organization.personId = userId;
                dataBase.OrganizationRegistration(organization);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }
    }
}