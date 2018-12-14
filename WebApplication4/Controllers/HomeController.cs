﻿using WebApplication4.Models.DBentities;
using WebApplication4.Models;
using System;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace Schedule.Controllers
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
        public string Organizationreg([Microsoft.AspNetCore.Mvc.FromBody] Organization organization)
        {
            try
            {
                User user = organization.head;
                long userId = dataBase.UserRegistration(user);
                organization.head.id = userId;
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
                long id  = dataBase.AddCource(course);
                course.id = id;
                return JsonConvert.SerializeObject(course);
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/updatecourse")]
        public string UpdateCourse([Microsoft.AspNetCore.Mvc.FromBody] Course course)
        {
            try
            {
                dataBase.UpdateCourse(course);
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
                dataBase.DeleteCourse(course.id);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/getgroups")]
        public string GetGroups([Microsoft.AspNetCore.Mvc.FromBody] Course course)
        {
            try
            {
                List<Group> groups = dataBase.getGroupsByCourseId(course.id);
                return JsonConvert.SerializeObject(groups);
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/creategroup")]
        public string CreateGroup([Microsoft.AspNetCore.Mvc.FromBody] Group group)
        {
            try
            {
                long id = dataBase.CreateGroup(group);
                group.id = id;
                return JsonConvert.SerializeObject(group);
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/updategroup")]
        public string UpdateGroup([Microsoft.AspNetCore.Mvc.FromBody] Group group)
        {
            try
            {
                dataBase.UpdateGroup(group);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/deletegroup")]
        public string DeleteGroup([Microsoft.AspNetCore.Mvc.FromBody] Group group)
        {
            try
            {
                dataBase.DeleteGroup(group.id);
                return "true";
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/addroom")]
        public string AddRoom([Microsoft.AspNetCore.Mvc.FromBody] Room room)
        {
            try
            {
                long id = dataBase.AddRoom(room);
                room.id = id;
                return JsonConvert.SerializeObject(room);
            }
            catch (Exception e)
            {
                return "false";
            }
        }

        [HttpPost]
        [Route("/createschedule")]
        public string CreateSchedule([Microsoft.AspNetCore.Mvc.FromBody] WebApplication4.Models.Schedule schedule)
        {
            try
            {
                long id = dataBase.CreateSchedule(schedule);
                schedule.id = id;
                return JsonConvert.SerializeObject(schedule);
            }
            catch (Exception e)
            {
                return "false";
            }
        }
    }
}