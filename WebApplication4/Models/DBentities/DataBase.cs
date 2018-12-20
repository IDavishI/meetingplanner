using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models.DBentities
{
    public class DataBase : DaoI
    {
        MySqlConnection mySqlConnection = new MySqlConnection("Database=schedule;Data Source=localhost;User Id=root;Password=;SslMode=none;");

        public long UserRegistration(User user)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO person(login, password, email, role) VALUES('" + user.login + "','" + user.password + "','" + user.email + "','" + user.role + "');";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        public User getUserByLoginAndPassword(string login, string password)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, login, password, email, role FROM person WHERE login = '" + login + "' AND password = '" + password + "';";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            reader.Read();
            User user = new User(Int32.Parse(reader["id"].ToString()), reader["login"].ToString(), reader["password"].ToString(), reader["email"].ToString(), reader["role"].ToString());
            reader.Close();
            return user;
        }

        public List<Course> getCourcesByOrganizationId(long id)
        {
            if(mySqlConnection.State == 0){
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name, description FROM course WHERE organizationId = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                courses.Add(new Course(Int32.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["description"].ToString()));
            }
            reader.Close();
            return courses;
        }

        internal Course getCourseByGroupId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name, description FROM course WHERE id = (SELECT courseId FROM `group` WHERE id = " + id + ");";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            Course course = new Course();
            while (reader.Read())
            {
                course = new Course(Int32.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["description"].ToString());
            }
            reader.Close();
            return course;
        }

        internal List<Schedule> getSchedulesGroupId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, time, groupId, instructorId, roomId FROM schedule WHERE groupId =" + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Schedule> schedules = new List<Schedule>();
            while (reader.Read())
            {
                Schedule schedule = new Schedule(Int32.Parse(reader["id"].ToString()), reader["time"].ToString(), Int32.Parse(reader["groupId"].ToString()), Int32.Parse(reader["instructorId"].ToString()), Int32.Parse(reader["instructorId"].ToString()));
                schedules.Add(schedule);
            }
            reader.Close();
            return schedules;
        }

        internal void signUptoGroup(long userId, long groupId)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO usergroup(userId, groupId) VALUES(" + userId + "," + groupId  + ");";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Group> getGroupsByUserId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name FROM `group` WHERE id = (SELECT groupId FROM usergroup WHERE userId = " + id + ");";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Group> groups = new List<Group>();
            while (reader.Read())
            {
                groups.Add(new Group(Int32.Parse(reader["id"].ToString()), reader["name"].ToString()));
            }
            reader.Close();
            return groups;
        }

        public void OrganizationRegistration(Organization organization)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO organization(name, description, specialization, personId) VALUES('" + organization.name + "','" + organization.description + "','" + organization.specialization + "','" + organization.head.id + "');";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Course> searchCourses(string pattern)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name, description FROM course WHERE name LIKE '%" + pattern + "%' OR description LIKE '%" + pattern + "%';";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                courses.Add(new Course(Int32.Parse(reader["id"].ToString()), reader["name"].ToString(), reader["description"].ToString()));
            }
            reader.Close();
            return courses;
        }

        public long AddCource(Course course)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO course(name,description,organizationId) VALUES('" + course.name + "','" + course.description + "','" + course.organizationId + "');";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal void UpdateCourse(Course course)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE course SET name = '" + course.name + "', description = '" + course.description + "' WHERE id=" + course.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        public void DeleteCourse(long courseId)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM course WHERE id=" + courseId + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Group> getGroupsByCourseId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name FROM `group` WHERE courseId = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Group> groups = new List<Group>();
            while (reader.Read())
            {
                groups.Add(new Group(Int32.Parse(reader["id"].ToString()), reader["name"].ToString()));
            }
            reader.Close();
            return groups;
        }

        internal long CreateGroup(Group group)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO `group`(name,courseId) VALUES('" + group.name + "'," + group.courseId + ");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal void UpdateGroup(Group group)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE `group` SET name = '" + group.name + "' WHERE id=" + group.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal void DeleteGroup(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM `group` WHERE id=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
            mySqlCommand.CommandText = "DELETE FROM usergroup WHERE groupId=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Room> getRoomsByCourseId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, number, floor FROM room WHERE courseId = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Room> rooms = new List<Room>();
            while (reader.Read())
            {
                rooms.Add(new Room(Int32.Parse(reader["id"].ToString()), reader["number"].ToString(), Int32.Parse(reader["floor"].ToString())));
            }
            reader.Close();
            return rooms;
        }

        internal long AddRoom(Room room)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO room(number,floor,courseId) VALUES('" + room.number + "'," + room.floor + "," + room.courseId +");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal List<Instructor> getInstructorsCourseId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, firstName, secondName, specialization FROM instructor WHERE courseId =" + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Instructor> instructors = new List<Instructor>();
            while (reader.Read())
            {
                instructors.Add(new Instructor(Int32.Parse(reader["id"].ToString()), reader["firstName"].ToString(), reader["secondName"].ToString(), reader["specialization"].ToString()));
            }
            reader.Close();
            return instructors;
        }

        internal long AddInstructor(Instructor instructor)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO instructor(firstName,secondName,specialization, courseId) VALUES('" + instructor.firstName + "','" + instructor.secondName + "','" + instructor.specialization + "'," + instructor.courseId + ");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal void UpdateInstructor(Instructor instructor)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE instructor SET firstName = '" + instructor.firstName + "', secondName = '" + instructor.secondName + "', specialization = '" + instructor.specialization + "' WHERE id=" + instructor.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Schedule> getSchedulesCourseId(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, time, groupId, instructorId, roomId FROM schedule WHERE courceId =" + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<Schedule> schedules = new List<Schedule>();
            while (reader.Read())
            {
                Schedule schedule = new Schedule(Int32.Parse(reader["id"].ToString()), reader["time"].ToString(), Int32.Parse(reader["groupId"].ToString()), Int32.Parse(reader["instructorId"].ToString()), Int32.Parse(reader["instructorId"].ToString()));
                schedules.Add(schedule);
            }
            reader.Close();
            return schedules;
        }

        public Group getGroupById(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name FROM `group` WHERE id = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            Group group = new Group();
            while (reader.Read())
            { 
                group = new Group(Int32.Parse(reader["id"].ToString()), reader["name"].ToString());
            }
            reader.Close();
            return group;
        }

        public Room getRoomById(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, number, floor FROM room WHERE id = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            Room room = new Room();
            while (reader.Read())
            {
                room = new Room(Int32.Parse(reader["id"].ToString()), reader["number"].ToString(), Int32.Parse(reader["floor"].ToString()));
            }
            reader.Close();
            return room;
        }

        public Instructor getInstructorById(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, firstName, secondName, specialization FROM instructor WHERE id = " + id + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            Instructor instructor = new Instructor();
            while (reader.Read())
            {
               instructor = new Instructor(Int32.Parse(reader["id"].ToString()), reader["firstName"].ToString(), reader["secondName"].ToString(), reader["specialization"].ToString());
            }
            reader.Close();
            return instructor;
        }

        internal void DeleteInstructor(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM instructor WHERE id=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<User> searchPersons(string pattern)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, login FROM person WHERE login LIKE '%" + pattern + "%';";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User(Int32.Parse(reader["id"].ToString()), reader["login"].ToString()));
            }
            reader.Close();
            return users;
        }

        internal void UpdateSchedule(Schedule schedule)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE schedule SET time = '" + schedule.time + "', courceId = " + schedule.courseId + ", roomId = " + schedule.roomId + ", instructorId = " + schedule.instructorId + ", groupId = " + schedule.groupId + " WHERE id=" + schedule.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal void DeleteSchedule(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM schedule WHERE id=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal void DeleteRoom(long id)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM room WHERE id=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        public long CreateSchedule(Schedule schedule)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO schedule(time,roomId,courceId,groupId,instructorId) VALUES('" + schedule.time + "'," + schedule.roomId + "," + schedule.courseId +"," + schedule.groupId +"," + schedule.instructorId + ");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        public Organization getOrganizationByHead(long headId)
        {
            if (mySqlConnection.State == 0)
            {
                mySqlConnection.Open();
            }
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id,name, description, specialization FROM organization WHERE personId = " + headId + ";";
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            reader.Read();
            Organization organization = new Organization(Int32.Parse(reader["id"].ToString()),reader["name"].ToString(), reader["description"].ToString(), reader["specialization"].ToString());
            reader.Close();
            return organization;
        }

        
    }
}