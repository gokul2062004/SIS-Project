using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using assignment_2.entity;
using assignment_2.util;

namespace assignment_2.dao
{
    public class EnrollmentRepositoryImpl : IEnrollmentRepository
    {
        private string connStr;

        public EnrollmentRepositoryImpl()
        {
            connStr = DBPropertyUtil.GetConnectionString("SISDB");
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Enrollments (student_id, course_id, enrollment_date) " +
                               "VALUES (@StudentId, @CourseId, @Date)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", enrollment.Student.StudentId);
                cmd.Parameters.AddWithValue("@CourseId", enrollment.Course.CourseId);
                cmd.Parameters.AddWithValue("@Date", enrollment.EnrollmentDate);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Enrollments WHERE enrollment_id=@EnrollmentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> list = new List<Enrollment>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Enrollments";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Enrollment enroll = new Enrollment(
                        Convert.ToInt32(reader["enrollment_id"]),
                        new Student(Convert.ToInt32(reader["student_id"]), "", "", DateTime.Now, "", ""),
                        new Course(Convert.ToInt32(reader["course_id"]), "", "", ""),
                        Convert.ToDateTime(reader["enrollment_date"])
                    );
                    list.Add(enroll);
                }
            }

            return list;
        }
    }
}
