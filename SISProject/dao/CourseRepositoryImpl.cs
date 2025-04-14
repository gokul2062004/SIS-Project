using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using assignment_2.entity;
using assignment_2.util;

namespace assignment_2.dao
{
    public class CourseRepositoryImpl : ICourseRepository
    {
        private string connStr;

        public CourseRepositoryImpl()
        {
            connStr = DBPropertyUtil.GetConnectionString("SISDB");
        }

        public void AddCourse(Course course)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Courses (course_name, course_code) VALUES (@CourseName, @CourseCode)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Courses SET course_name=@CourseName, course_code=@CourseCode WHERE course_id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCourse(int courseId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Courses WHERE course_id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public Course GetCourseById(int courseId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Courses WHERE course_id=@CourseId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Course(
                        Convert.ToInt32(reader["course_id"]),
                        reader["course_name"].ToString(),
                        reader["course_code"].ToString(),
                        ""
                    );
                }
            }
            return null;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Courses";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courses.Add(new Course(
                        Convert.ToInt32(reader["course_id"]),
                        reader["course_name"].ToString(),
                        reader["course_code"].ToString(),
                        ""
                    ));
                }
            }
            return courses;
        }
    }
}
