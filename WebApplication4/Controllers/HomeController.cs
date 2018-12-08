using MeetingPlanner.Models.DBentities;
using Schedule.Models;
using System;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace MeetingPlanner.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class HomeController : ControllerBase
    {
        DataBase dataBase = new DataBase();

        [HttpPost]
        [Route("/reg")]
        public string Registration([FromBody] User user)
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
        public string Authorization([Microsoft.AspNetCore.Mvc.FromBody] User user)
        {
            try
            {
                user = dataBase.getUserByLoginAndPassword(user.login, user.password);
                if (user.role.Contains("ROLE_ADMIN"))
                {
                    Organization organization = dataBase.getOrganizationByHead(user.id);
                    organization.head = user;
                    List<Course> courses = dataBase.getCourcesByOrganizationId(organization.id);
                    organization.courses = courses;
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
        public string Organizationreg([Microsoft.AspNetCore.Mvc.FromQuery] User user,
                                      [Microsoft.AspNetCore.Mvc.FromQuery] Organization organization)
        {
            try
            {
                long userId = dataBase.UserRegistration(user);
                organization.personId = userId;
                dataBase.OrganizationRegistration(organization);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/addcourse")]
        public string AddCourse([Microsoft.AspNetCore.Mvc.FromBody] Course course)
        {
            try
            {
                dataBase.AddCource(course);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/deletecourse")]
        public string DeleteCourse([Microsoft.AspNetCore.Mvc.FromBody] Course course)
        {
            try
            {
                dataBase.DeleteCource(course.id);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }
    }
}