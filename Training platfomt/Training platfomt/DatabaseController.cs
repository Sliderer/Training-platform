using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;


namespace Training_platfomt
{
    class DatabaseController
    {
        private SqlConnection sqlConnection;
        private readonly string connectionstring = @"Data Source=LAPTOP-10LBF8FV\SLIDERER;Initial Catalog=TrainingPlatform;Integrated Security=True;";

        public DatabaseController()
        {
            sqlConnection = new SqlConnection(connectionstring);
            sqlConnection.Open();
        }

        public User FindUser(string login)
        {
            User user = null;
            SqlCommand command = new SqlCommand($"select * from Users where login = '{login}'", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                user = new User(Convert.ToInt32(dataReader["id"]), dataReader["name"].ToString(), dataReader["surname"].ToString(),
                                   dataReader["login"].ToString(), dataReader["email"].ToString(), 
                                   dataReader["password"].ToString());
            }
            dataReader.Close();
            return user;
        }

        public void AddUser(User user)
        {
            SqlCommand command = new SqlCommand($"insert into Users (name, surname, email, password, login)" +
                $"values ('{user.name}', '{user.surname}', '{user.email}', '{user.password}', '{user.login}')", sqlConnection);
            command.ExecuteNonQuery();

            AddNewRoll(user);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            SqlCommand command = new SqlCommand("select * from Courses", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                yield return new Course() {id = Convert.ToInt32(dataReader["id"]), discription = dataReader["discription"].ToString(),
                                           title = dataReader["title"].ToString()};
            }
            dataReader.Close();
        }

        public Course GetCourse(string title)
        {
            Course course = null;
            SqlCommand command = new SqlCommand($"select * from Courses where title = '{title}'", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                course = new Course() { id = Convert.ToInt32(dataReader["id"]), title = dataReader["title"].ToString(), 
                                        discription = dataReader["discription"].ToString()};
            }
            dataReader.Close();
            return course;
        }

        public IEnumerable<Video> GetCourseVideos(int course_id)
        {
            SqlCommand command = new SqlCommand($"select * from Videos where course_id = {course_id}", sqlConnection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                yield return new Video() { id = Convert.ToInt32(dataReader["id"]), title = dataReader["title"].ToString(),
                                           link = dataReader["link"].ToString(), course_id = Convert.ToInt32(dataReader["course_id"])};
            }
            dataReader.Close();
        }

        private void AddNewRoll(User user)
        {
            int id = FindUser(user.login).id;
            SqlCommand command = new SqlCommand($"insert into Roles (user_id, roll) values ({id}, 'user')", sqlConnection);
            command.ExecuteNonQuery();
        } 

        ~DatabaseController()
        {
            sqlConnection.Close();
        }
    }
}
