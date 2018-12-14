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
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO person(login, password, email, role) VALUES('" + user.login + "','" + user.password + "','" + user.email + "','" + user.role + "');";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        public User getUserByLoginAndPassword(string login, string password)
        {
            mySqlConnection.Open();

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

        public void OrganizationRegistration(Organization organization)
        {
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO organization(name, description, specialization, personId) VALUES('" + organization.name + "','" + organization.description + "','" + organization.specialization + "','" + organization.head.id + "');";
            mySqlCommand.ExecuteNonQuery();
        }

        public long AddCource(Course course)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO course(name,description,organizationId) VALUES('" + course.name + "','" + course.description + "','" + course.organizationId + "');";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal void UpdateCourse(Course course)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE course SET name = " + course.name + ", description = " + course.description + " WHERE id=" + course.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        public void DeleteCourse(long courseId)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM course WHERE id=" + courseId + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal List<Group> getGroupsByCourseId(long id)
        {
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "SELECT id, name FROM groups WHERE courseId = " + id + ";";
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
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO group(name,courseId) VALUES('" + group.name + "',"+ group.courseId +");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        internal void UpdateGroup(Group group)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "UPDATE group SET name = " + group.name + " WHERE id=" + group.id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal void DeleteGroup(long id)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "DELETE FROM group WHERE id=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
            mySqlCommand.CommandText = "DELETE FROM usergroup WHERE groupId=" + id + ";";
            mySqlCommand.ExecuteNonQuery();
        }

        internal long AddRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public long CreateSchedule(Schedule schedule)
        {
            mySqlConnection.Open();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = "INSERT INTO schedule(time,roomId,courceId,groupId,instructorId) VALUES('" + schedule.time + "'," + schedule.roomId + "," + schedule.courseId +"," + schedule.groupId +"," + schedule.instructorId + ");";
            mySqlCommand.ExecuteNonQuery();
            return mySqlCommand.LastInsertedId;
        }

        public Organization getOrganizationByHead(long headId)
        {
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