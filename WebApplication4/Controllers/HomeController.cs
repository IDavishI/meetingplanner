using WebApplication4.Models.DBentities;
using WebApplication4.Models;
using System;
using System.Web;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;
using WebApiContrib.Formatting;
using Newtonsoft.Json.Linq;

namespace Schedule.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class HomeController : ControllerBase
    {
        DataBase dataBase = new DataBase();

        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            DateParseHandling = DateParseHandling.None,
        };

        [HttpPost]
        [Route("/reg")]
        public IActionResult Registration([FromBody] User user)
        {
            try
            {
                dataBase.UserRegistration(user);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/auth")]
        public IActionResult Authorization([FromBody] User user)
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
                    return Content(JsonConvert.SerializeObject(organization));
                }
                return Content(JsonConvert.SerializeObject(user));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/organizationreg")]
        public IActionResult Organizationreg([FromBody] Organization organization)
        {
            try
            {
                User user = organization.head;
                long userId = dataBase.UserRegistration(user);
                organization.head.id = userId;
                dataBase.OrganizationRegistration(organization);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/searchcourses")]
        public IActionResult SearchCourses([FromBody]JObject pattern)
        {
            try
            {
                string str = pattern.Value<string>("pattern");
                List<Course> courses = dataBase.searchCourses(str);
                return Content(JsonConvert.SerializeObject(courses));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        [Route("/getcourses")]
        public IActionResult GetCourses([FromBody] Organization organization)
        {
            try
            {
                List<Course> courses = dataBase.getCourcesByOrganizationId(organization.id);
                return Content(JsonConvert.SerializeObject(courses));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }


        [HttpPost]
        [Route("/addcourse")]
        public IActionResult AddCourse([FromBody] Course course)
        {
            try
            {
                long id  = dataBase.AddCource(course);
                course.id = id;
                return Content(JsonConvert.SerializeObject(course));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/updatecourse")]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            try
            {
                dataBase.UpdateCourse(course);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/deletecourse")]
        public IActionResult DeleteCourse([FromBody] Course course)
        {
            try
            {
                dataBase.DeleteCourse(course.id);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/searchperson")]
        public IActionResult SearchPersons([FromBody]JObject pattern)
        {
            try
            {
                string str = pattern.Value<string>("pattern");
                List<User> users = dataBase.searchPersons(str);
                return Content(JsonConvert.SerializeObject(users));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/getgroups")]
        public IActionResult GetGroups([FromBody] Course course)
        {
            try
            {
                List<Group> groups = dataBase.getGroupsByCourseId(course.id);
                return Content(JsonConvert.SerializeObject(groups));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/creategroup")]
        public IActionResult CreateGroup([FromBody] Group group)
        {
            try
            {
                long id = dataBase.CreateGroup(group);
                group.id = id;
                return Content(JsonConvert.SerializeObject(group));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/updategroup")]
        public IActionResult UpdateGroup([FromBody] Group group)
        {
            try
            {
                dataBase.UpdateGroup(group);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/deletegroup")]
        public IActionResult DeleteGroup([FromBody] Group group)
        {
            try
            {
                dataBase.DeleteGroup(group.id);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/getrooms")]
        public IActionResult GetRooms([FromBody] Course course)
        {
            try
            {
                List<Room> rooms = dataBase.getRoomsByCourseId(course.id);
                return Content(JsonConvert.SerializeObject(rooms));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/addroom")]
        public IActionResult AddRoom([FromBody] Room room)
        {
            try
            {
                long id = dataBase.AddRoom(room);
                room.id = id;
                return Content(JsonConvert.SerializeObject(room));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/deleteroom")]
        public IActionResult DeleteRoom([FromBody] Room room)
        {
            try
            {
                dataBase.DeleteRoom(room.id);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/getinstructors")]
        public IActionResult GetInstructors([FromBody] Course course)
        {
            try
            {
                List<Instructor> instructors = dataBase.getInstructorsCourseId(course.id);
                return Content(JsonConvert.SerializeObject(instructors));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/addinstructors")]
        public IActionResult AddInstructors([FromBody] Instructor instructor)
        {
            try
            {
                long id = dataBase.AddInstructor(instructor);
                instructor.id = id;
                return Content(JsonConvert.SerializeObject(instructor));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }


        [HttpPost]
        [Route("/updateinstructor")]
        public IActionResult UpdateInstructor([FromBody] Instructor instructor)
        {
            try
            {
                dataBase.UpdateInstructor(instructor);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/deleteinstructor")]
        public IActionResult DeleteInstructor([FromBody] Instructor instructor)
        {
            try
            {
                dataBase.DeleteInstructor(instructor.id);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }


        [HttpPost]
        [Route("/getschedule")]
        public IActionResult GetSchedule([FromBody] Course course)
        {
            try
            {
                List<WebApplication4.Models.Schedule> schedules = dataBase.getSchedulesCourseId(course.id);
                foreach(WebApplication4.Models.Schedule schedule in schedules){
                    schedule.group = dataBase.getGroupById(schedule.groupId);
                    schedule.room = dataBase.getRoomById(schedule.roomId);
                    schedule.Instructor = dataBase.getInstructorById(schedule.instructorId);
                }
                return Content(JsonConvert.SerializeObject(schedules));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }



        [HttpPost]
        [Route("/createschedule")]
        public IActionResult CreateSchedule([FromBody] WebApplication4.Models.Schedule schedule)
        {
            try
            {
                long id = dataBase.CreateSchedule(schedule);
                schedule.id = id;
                return Content(JsonConvert.SerializeObject(schedule));
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/updateschedule")]
        public IActionResult UpdateSchedule([FromBody] WebApplication4.Models.Schedule schedule)
        {
            try
            {
                dataBase.UpdateSchedule(schedule);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }

        [HttpPost]
        [Route("/deleteschedule")]
        public IActionResult DeleteSchedule([FromBody] WebApplication4.Models.Schedule schedule)
        {
            try
            {
                dataBase.DeleteSchedule(schedule.id);
                return Content("true");
            }
            catch (Exception e)
            {
                return Content("false");
            }
        }



    }
}